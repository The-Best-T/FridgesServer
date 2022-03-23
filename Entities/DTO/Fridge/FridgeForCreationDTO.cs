using Entities.ValidateAttributes;
using System;
namespace Entities.DTO.Fridge
{
    public class FridgeForCreationDTO : FridgeForManipulationDTO
    {
        [NotEmptyGuid]
        public Guid ModelId { get; set; }
    }
}
