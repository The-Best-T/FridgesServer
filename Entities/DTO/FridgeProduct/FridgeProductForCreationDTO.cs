using Entities.ValidateAttributes;
using System;
namespace Entities.DTO.FridgeProduct
{
    public class FridgeProductForCreationDTO : FridgeProductForManipulationDTO
    {
        [NotEmptyGuid]
        public Guid ProductId { get; set; }
    }
}
