using System;
using System.ComponentModel.DataAnnotations;
namespace Entities.DTO.FridgeProduct
{
    public class FridgeProductForCreationDTO : FridgeProductForManipulationDTO
    {
        [Required(ErrorMessage ="ProductId is Required field.")]
        public Guid ProductId { get; set; }
    }
}
