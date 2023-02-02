using DocManagement.Core.Enums;
using DocManagement.Core.Models;
using DocManagements.AppServices.Interfaces;
using DocManagements.AppServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DocManagements.Web.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _documentService.GetDocumentsAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel document)
        {
            if (!ModelState.IsValid) return View(document);

            try
            {
                await _documentService.RegisterNewDocumentAsync(document);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                ModelState.AddModelError("", ex.Message);
                return View(document);
            }


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(long Id)
        {
            var document = await _documentService.GetByIdAsync(Id);
            return View(document);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DocumentViewModel document)
        {
            if (!ModelState.IsValid) return View(document);
            try
            {
                var result = await _documentService.UpdateDocument(document);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(document);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long Id)
        {
            var document = await _documentService.GetByIdAsync(Id);
            return View(document);
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var document = await _documentService.GetByIdAsync(Id);
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(long DocumentId)
        {
            await _documentService.DeleteDocumentAsync(DocumentId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(long Id)
        {
            var document = await _documentService.GetByIdAsync(Id);
            var documentInBytes = await _documentService.FileInBytes(Id);

            return File(documentInBytes, "application/force-download", document.fileName);
        }
    }
}
