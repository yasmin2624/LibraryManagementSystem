using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.Models.Payment
{
    public class UpdatePaymentDto
    {
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}