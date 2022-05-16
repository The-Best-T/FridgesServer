using System;

namespace Entities.Dto.FridgeProduct
{
    public class FridgeProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
