using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.FridgeProduct
{
    public abstract class FridgeProductForManipulationDto
    {
        [Range(0, 99, ErrorMessage = "Quantity is required and it must be in range[0,99].")]
        public int Quantity { get; set; }
    }
}
