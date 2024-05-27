using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLDVpoe.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "Invalid price format")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Availability { get; set; }

     
    }
}
