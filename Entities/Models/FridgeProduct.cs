using System;
using System.ComponentModel.DataAnnotations;
namespace Entities.Models
{
    public class FridgeProduct
    {
        [Required(ErrorMessage = "Product id is a required field.")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "Fridge id is a required field.")]
        public Guid FridgeId { get; set; }
        public virtual Fridge Fridge { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        [Range(0,99,ErrorMessage ="Quantity must be in range(0,99).")]
        public int Quantity { get; set; }
    }
}
