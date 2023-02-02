using DocManagement.Core.Interfaces;
using DocManagements.AppServices.Interfaces;
using DocManagements.AppServices.Services;
using DocManagements.Infra.Data.Context;
using DocManagements.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace DocManagements.Infra.CrossCutting.IoT
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IDocumentService, DocumentService>();

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<ApplicationDbContext>();

        }
    }
}
