using ITServiceManagement.API.Models;
using System.ComponentModel.DataAnnotations;
namespace ITServiceManagement.API.DTOs

{
    public class CreateAssetDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength (50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public AssetStatus Status { get; set; }
    }
}