using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO.FridgeModel;
namespace Entities.DTO.Fridge
{
    public  class FridgeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public FridgeModelDTO Model { get; set; }
    }
}
