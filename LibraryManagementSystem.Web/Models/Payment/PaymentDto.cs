namespace LibraryManagementSystem.Web.Models.Payment
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int BorrowID { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}