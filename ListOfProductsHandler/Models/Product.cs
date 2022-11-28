using System.ComponentModel.DataAnnotations;

namespace ListOfProductsHandler.Models
{
    public class Product
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public ushort Price { get; set; }
    }
}
