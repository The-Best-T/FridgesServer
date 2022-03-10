using System;
namespace Entities.Models
{
    public class FridgeProduct : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid FridgeId { get; set; }
        public Fridge Fridge { get; set; }
        public int Quantity { get; set; }
    }
}
