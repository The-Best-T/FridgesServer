using System.ComponentModel.DataAnnotations;
namespace Entities.Dto.FridgeModel
{
    public abstract class FridgeModelForManipulationDto
    {
        [Required(ErrorMessage = "Fridge model name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Fridge model name is 30 characters.")]
        public string Name { get; set; }

        [Range(1900, int.MaxValue, ErrorMessage = "Year is required and it can't be lower than 1900")]
        public int Year { get; set; }
    }
}
