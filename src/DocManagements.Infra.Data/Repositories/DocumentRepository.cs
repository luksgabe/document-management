using DocManagement.Core.Entities;
using DocManagement.Core.Interfaces.Repositories;
using DocManagements.Infra.Data.Context;

namespace DocManagements.Infra.Data.Repositories
{
    public class DocumentRepository : BaseRepository<Documentt>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
            context.ChangeTracker.LazyLoadingEnabled = true;
        }
    }
}
