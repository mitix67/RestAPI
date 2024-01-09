using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class CreateRestaurantDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public bool HasDelivery { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
