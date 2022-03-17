using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Product
    {
        [Column("ProductId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Range(0,99,ErrorMessage ="DefaultQuantity must be in range(0,99).")]
        public int DefaultQuantity { get; set; }
        public virtual ICollection<Fridge> Fridges { get; set; }
        public virtual ICollection<FridgeProduct> FridgeProducts { get; set; }

    }
}
