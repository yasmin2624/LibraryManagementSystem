using LibraryManagementSystem.Application.DTOs.Payment;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    BorrowID = p.BorrowID,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status,
                    Notes = p.Notes
                })
                .ToListAsync();
        }

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Where(p => p.PaymentID == id)
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    BorrowID = p.BorrowID,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status,
                    Notes = p.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
        {
            var borrow = await _context.Borrows.FindAsync(dto.BorrowID);

            if (borrow == null)
                throw new Exception("Borrow record not found.");

            var payment = new Payment
            {
                BorrowID = dto.BorrowID,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = dto.PaymentDate,
                Status = dto.Status,
                Notes = dto.Notes
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentDto
            {
                PaymentID = payment.PaymentID,
                BorrowID = payment.BorrowID,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                Notes = payment.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdatePaymentDto dto)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return false;

            payment.Amount = dto.Amount;
            payment.PaymentMethod = dto.PaymentMethod;
            payment.PaymentDate = dto.PaymentDate;
            payment.Status = dto.Status;
            payment.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PaymentDto>> GetByBorrowIdAsync(int borrowId)
        {
            return await _context.Payments
                .Where(p => p.BorrowID == borrowId)
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    BorrowID = p.BorrowID,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status,
                    Notes = p.Notes
                })
                .ToListAsync();
        }
    }
}
