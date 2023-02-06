using DocManagement.Core.Enums;
using DocManagement.Core.Interfaces;
using DocManagement.Core.Interfaces.Repositories;
using DocManagements.AppServices.Interfaces;
using DocManagements.AppServices.Services;
using DocManagements.AppServices.ViewModels;
using DocManagements.Infra.Data.Context;
using DocManagements.Infra.Data.UoW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DocManagements.AppServices.Test
{
    public class DocumentServiceTest
    {
        private readonly IDocumentService _documentService;
        private readonly IWebHostEnvironment _webHost;

        public DocumentServiceTest()
        {
            var uow = new Mock<IUnitOfWork>().Object;
            _webHost = new Mock<IWebHostEnvironment>().Object;
            _documentService = new DocumentService(uow, _webHost);
        }

        [Fact]
        public void RegisterNewDocumentAsync_DocumentIsNotValid()
        {
            IFormFile file = fakeFile();

            var newDocument = new DocumentViewModel
            {
                DocumentId = 0,
                Description = "teste",
                File = file,
                fileName = file.FileName,
                Name = "Sr Teste",
                Status = Status.Pending
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _documentService.RegisterNewDocumentAsync(newDocument));  
        }

        [Fact]
        public void GetDocumentAsync()
        {          
            var documentId = 3;

            DocumentViewModel result = _documentService.GetByIdAsync(documentId).Result;
            Assert.NotNull(result);
        }

        private FormFile fakeFile()
        {
            var content = "Eu sou um arquivo fake";
            var fileName = "teste.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return new FormFile(stream, 0, stream.Length, "APPLICATION/octet-stream", fileName);
        }
    }
}
