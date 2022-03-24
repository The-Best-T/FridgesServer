using Entities.ValidateAttributes;
using System;
namespace Entities.Dto.FridgeProduct
{
    public class FridgeProductForCreationDto : FridgeProductForManipulationDto
    {
        [NotEmptyGuid]
        public Guid ProductId { get; set; }
    }
}
