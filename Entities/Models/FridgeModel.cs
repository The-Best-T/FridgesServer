using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class FridgeModel
    {
        [Column("FridgeModelId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Fridge> Fridges { get; set; }
    }
}
