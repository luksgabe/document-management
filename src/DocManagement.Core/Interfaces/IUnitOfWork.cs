using DocManagement.Core.Interfaces.Repositories;
using FluentValidation.Results;
namespace DocManagement.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IDocumentRepository documentRepository { get; }
        Task<bool> Commit();
    }
}
