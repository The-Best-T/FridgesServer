using System;

namespace Entities.DTO.Product
{
    public class FridgeProductDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
