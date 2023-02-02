using DocManagement.Core.Interfaces;
using DocManagement.Core.Interfaces.Repositories;
using DocManagements.Infra.Data.Context;
using DocManagements.Infra.Data.Repositories;

namespace DocManagements.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDocumentRepository _documentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDocumentRepository documentRepository => _documentRepository ?? new DocumentRepository(_context);

        public async Task<bool> Commit()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
