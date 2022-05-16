using System;
using Entities.Dto.FridgeModel;
namespace Entities.Dto.Fridge
{
    public class FridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public FridgeModelDto Model { get; set; }
    }
}
