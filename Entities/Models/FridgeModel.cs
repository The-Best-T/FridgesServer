using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class FridgeModel : IBaseEntity<Guid>
    {
        [Column("FridgeModelId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
