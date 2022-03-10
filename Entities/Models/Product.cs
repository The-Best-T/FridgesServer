using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Product : IBaseEntity<Guid>
    {
        [Column("ProductId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaulQuantity { get; set; }
        public List<Fridge> Fridges { get; set; } = new();
        public List<FridgeProduct> Positions { get; set; } = new();

    }
}
