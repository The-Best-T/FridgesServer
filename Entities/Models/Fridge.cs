using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Fridge
    {

        [Column("FridgeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the Owner name is 30 characters.")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Model id is a required field.")]
        public Guid ModelId { get; set; }
        public virtual FridgeModel Model { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<FridgeProduct> FridgeProducts { get; set; }
    }
}
