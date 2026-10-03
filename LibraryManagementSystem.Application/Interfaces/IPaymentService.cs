using LibraryManagementSystem.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();

        Task<PaymentDto?> GetByIdAsync(int id);

        Task<PaymentDto> CreateAsync(CreatePaymentDto dto);

        Task<bool> UpdateAsync(int id, UpdatePaymentDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PaymentDto>> GetByBorrowIdAsync(int borrowId);
    }
}
