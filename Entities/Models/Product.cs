using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Product
    {
        [Column("ProductId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultQuantity { get; set; }
        public virtual ICollection<Fridge> Fridges { get; set; }
        public virtual ICollection<FridgeProduct> FridgeProducts { get; set; }

    }
}
