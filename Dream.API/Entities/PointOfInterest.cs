using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dream.API.Entities
{
    public class PointOfInterest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        //Make sure that Name is not Null. 
        public PointOfInterest(string name)
        {
            this.Name = name;   
        }

        #region Relations

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }

        #endregion
    }
}
