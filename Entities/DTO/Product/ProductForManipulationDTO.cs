using System.ComponentModel.DataAnnotations;
namespace Entities.DTO.Product
{
    public abstract class ProductForManipulationDTO
    {
        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Product name is 30 characters.")]
        public string Name { get; set; }

        [Range(1, 99, ErrorMessage = "DefaultQuantity is required and it must be in range(1,99).")]
        public int DefaultQuantity { get; set; }
    }
}
