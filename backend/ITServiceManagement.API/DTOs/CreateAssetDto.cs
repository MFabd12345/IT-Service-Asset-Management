using System.ComponentModel.DataAnnotations;
namespace ITServiceManagement.API.DTOs

{
    public class CreateAssetDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}