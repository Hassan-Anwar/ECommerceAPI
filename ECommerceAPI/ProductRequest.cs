using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI
{
    public class ProductRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}