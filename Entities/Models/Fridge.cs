using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Fridge
    {

        [Column("FridgeId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public Guid ModelId { get; set; }
        public virtual FridgeModel Model { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<FridgeProduct> FridgeProducts { get; set; }
    }
}
