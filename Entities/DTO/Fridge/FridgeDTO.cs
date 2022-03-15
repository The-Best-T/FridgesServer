using System;
namespace Entities.DTO.Fridge
{
    public class FridgeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string ModelName { get; set; }
    }
}
