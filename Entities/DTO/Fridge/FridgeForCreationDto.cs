using Entities.ValidateAttributes;
using System;
namespace Entities.Dto.Fridge
{
    public class FridgeForCreationDto : FridgeForManipulationDto
    {
        [NotEmptyGuid]
        public Guid ModelId { get; set; }
    }
}
