using System;
namespace Entities.Models
{
    public class FridgeProduct
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid FridgeId { get; set; }
        public virtual Fridge Fridge { get; set; }
        public int Quantity { get; set; }
    }
}
