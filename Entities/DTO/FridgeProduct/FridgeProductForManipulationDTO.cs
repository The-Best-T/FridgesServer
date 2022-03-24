using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.FridgeProduct
{
    public abstract class FridgeProductForManipulationDto
    {
        [Range(1, 99, ErrorMessage = "Quantity is required and it must be in range(1,99).")]
        public int Quantity { get; set; }
    }
}
