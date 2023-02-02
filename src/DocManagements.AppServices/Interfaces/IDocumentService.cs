using DocManagements.AppServices.ViewModels;

namespace DocManagements.AppServices.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentViewModel> GetByIdAsync(long id);
        Task<IEnumerable<DocumentViewModel>> GetDocumentsAsync();
        Task RegisterNewDocumentAsync(DocumentViewModel document);
        Task<DocumentViewModel> UpdateDocument(DocumentViewModel document);
        Task DeleteDocumentAsync(long id);
        Task<byte[]> FileInBytes(long id);
    }
}
