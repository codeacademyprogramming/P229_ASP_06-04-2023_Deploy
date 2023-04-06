using System.ComponentModel.DataAnnotations;

namespace P229Allup.Models
{
    public class Brand : BaseEntity
    {
        [StringLength(255,ErrorMessage ="Qaqa Maksimum 255 eee")]
        [Required(ErrorMessage ="Mejburidi Qaqa")]
        public string Name { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
