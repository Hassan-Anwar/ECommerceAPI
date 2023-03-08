using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI
{
    public class OrderRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public List<ProductRequest> Products { get; set; }
    }
}