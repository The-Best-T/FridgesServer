using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.FridgeProduct
{
    public abstract class FridgeProductForManipulationDTO
    {
        [Range(1, 99, ErrorMessage = "Quantity is required and it must be in range(1,99).")]
        public int Quantity { get; set; }
    }
}
