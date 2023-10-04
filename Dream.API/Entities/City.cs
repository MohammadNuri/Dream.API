using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.API.Entities
{
    public class City
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        //Make sure that Name is not Null. 
        public City(string name)
        {
            this.Name = name;
        }

        #region Relations

        public ICollection<PointOfInterest> PointOfInterest { get; set; } = new List<PointOfInterest>();

        #endregion


    }
}
