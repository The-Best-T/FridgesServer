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
        public int DefaulQuantity { get; set; }
        public List<Fridge> Fridges { get; set; } = new();
        public List<FridgeProduct> FridgeProducts { get; set; } = new();

    }
}
