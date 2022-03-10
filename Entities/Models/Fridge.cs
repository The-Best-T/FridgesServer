using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Fridge : IBaseEntity<Guid>
    {
        [Column("FridgeId")]
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        [Column("ModelId")]
        public FridgeModel FridgeModel { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<FridgeProduct> FridgeProducts { get; set; } = new();
    }
}
