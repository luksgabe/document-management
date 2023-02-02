using DocManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace DocManagements.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        private readonly IConfiguration _configuration;

        public string _connectionString { get; set; }

        public DbSet<Documentt> Documentt { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddMappingsDynamically(modelBuilder);
        }

        private static void AddMappingsDynamically(ModelBuilder modelBuilder)
        {
            var currentAssembly = typeof(ApplicationDbContext).Assembly;
            var efMappingTypes = currentAssembly.GetTypes().Where(t =>
                t.FullName.StartsWith("DocManagements.Infra.Data.Mapping.") &&
                t.FullName.EndsWith("Map"));

            foreach (var map in efMappingTypes.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)map);
            }
        }

    }
}
