using DocManagement.Core.Enums;
using DocManagement.Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocManagements.AppServices.ViewModels
{
    public class DocumentViewModel
    {
        public long DocumentId { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Description { get; set; }
        public Status Status { get; set; }

        public IFormFile? File { get; set; }

        [DisplayName("Nome do arquivo")]
        public string? fileName { get; set; }
    }
}
