using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }

        public int BorrowID { get; set; }

        public decimal Amount { get; set; }

        public string? PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }

        public Borrow Borrow { get; set; } = null!;
    }
}
