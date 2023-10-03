using System.ComponentModel.DataAnnotations;

namespace Dream.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "The Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
