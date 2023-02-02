using DocManagement.Core.Entities;
using DocManagement.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocManagements.Infra.Data.Mapping
{
    public class DocumentMap : IEntityTypeConfiguration<Documentt>
    {
        public void Configure(EntityTypeBuilder<Documentt> builder)
        {
            builder.ToTable("Document");
            
            builder.HasKey(t => t.DocumentId);

            builder.Property(d => d.DocumentId)
                .HasColumnName("DocumentId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(d => d.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(d => d.Description)
                .HasColumnType("varchar(360)")
                .IsRequired();

            builder.Property(d => d.Status)
                .HasColumnType("tinyint")
                .HasDefaultValue(Status.Pending);

            builder.Property(d => d.Url)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder.Property(e => e.Created_At)
               .HasColumnType("datetime")
               .HasColumnName("Created_At");

            builder.Property(e => e.Updated_At)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At")
                .IsRequired(false);

            builder.Ignore(e => e.Id);
        }
    }
}
