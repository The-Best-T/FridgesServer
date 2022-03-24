using System;
using Entities.DTO.FridgeModel;
namespace Entities.DTO.Fridge
{
    public class FridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public FridgeModelDTO Model { get; set; }
    }
}
