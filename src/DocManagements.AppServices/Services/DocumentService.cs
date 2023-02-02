using AutoMapper;
using DocManagement.Core.Entities;
using DocManagement.Core.Enums;
using DocManagement.Core.Interfaces;
using DocManagements.AppServices.Interfaces;
using DocManagements.AppServices.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DocManagements.AppServices.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _env;
        public DocumentService(IUnitOfWork unitofWork, IWebHostEnvironment env)
        {
            _env = env;
            _unitofWork = unitofWork;
        }


        public async Task<DocumentViewModel> GetByIdAsync(long id)
        {
            var document = await _unitofWork.documentRepository.GetByIdAsync(id);
            var documentViewModel = new DocumentViewModel
            {
                DocumentId = document.Id,
                Description = document.Description,
                Name = document.Name,
                Status = document.Status,
            };

            documentViewModel.File = getFile(document.Url);
            documentViewModel.fileName =documentViewModel.File.FileName;
            return documentViewModel;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetDocumentsAsync()
        {
            var documents = await _unitofWork.documentRepository.GetAllAsync();

            return documents.Select(x => new DocumentViewModel
            {
                DocumentId = x.DocumentId,
                Description = x.Description,
                Name = x.Name,
                Status = x.Status,
            }).ToList();
        }

        public async Task RegisterNewDocumentAsync(DocumentViewModel document)
        {
            var url = await uploadNewDocument(document.File);

            var newDocument = new Documentt(
                document.Name,
                document.Description,
                Status.Pending,
                url
            );

            await _unitofWork.documentRepository.AddAsync(newDocument);
            await _unitofWork.Commit();
        }

        public async Task<DocumentViewModel> UpdateDocument(DocumentViewModel documentUpdate)
        {
            Documentt document = await _unitofWork.documentRepository.GetByIdAsync(documentUpdate.DocumentId);
            var url = document.Url;

            if(documentUpdate.File != null)
            {
                deleteFile(document.Url);
                url = await uploadNewDocument(documentUpdate.File);
            }

            document.Update(documentUpdate.Name, documentUpdate.Description, documentUpdate.Status, url);

            await _unitofWork.documentRepository.UpdateAsync(document);
            await _unitofWork.Commit();
            return documentUpdate;
        }


        public async Task DeleteDocumentAsync(long id)
        {
            Documentt document = await _unitofWork.documentRepository.GetByIdAsync(id);

            deleteFile(document.Url);

            _unitofWork.documentRepository.Remove(id);
            await _unitofWork.Commit();
        }

        private void deleteFile(string url)
        {
            var dir = Path.Combine(_env.WebRootPath, string.Concat("uploads//", url));
            FileInfo file = new FileInfo(dir);
            if (!file.Exists) throw new Exception("Arquivo não existe.");
            file.Delete();
        }

        public async Task<byte[]> FileInBytes(long id)
        {
            Documentt document = await _unitofWork.documentRepository.GetByIdAsync(id);
            var filePath = Path.Combine(_env.WebRootPath, string.Concat("uploads//", document.Url));
            return File.ReadAllBytes(filePath);
        }

        #region private methods

        private IFormFile getFile(string fileName)
        {

            var dir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(dir)) throw new Exception("Diretório não existe");

            var fileFullName = Path.Combine(dir + fileName);

            if (!File.Exists(fileFullName)) throw new Exception("Arquivo não existe");

            var net = new System.Net.WebClient();
            var data = net.DownloadData(fileFullName);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";

            string formatedName = getFormatedFileName(fileName);

            return new FormFile(content, 0, content.Length, contentType, formatedName);
        }
        private string getFormatedFileName(string fileName)
        {
            var splited = fileName.Split("__");
            return splited[1];
        }

        private async Task<string> uploadNewDocument(IFormFile file)
        {
            var fileName = string.Empty;
            var dir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (file.Length > 0)
            {
                fileName = Path.DirectorySeparatorChar + System.Guid.NewGuid().ToString("N") + "__" + file.FileName;
                using (var stream = new FileStream(dir + fileName, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return fileName;
        }

  
        #endregion

    }
}
