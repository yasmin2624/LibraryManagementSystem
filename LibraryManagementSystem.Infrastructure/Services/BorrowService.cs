using LibraryManagementSystem.Application.DTOs.Borrow;
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
    public class BorrowService : IBorrowService
    {
        private readonly ApplicationDbContext _context;

        public BorrowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowDto>> GetAllAsync()
        {
            return await _context.Borrows
                .Select(b => new BorrowDto
                {
                    BorrowID = b.BorrowID,
                    BookID = b.BookID,
                    MemberID = b.MemberID,
                    LibrarianID = b.LibrarianID,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                })
                .ToListAsync();
        }

        public async Task<BorrowDto?> GetByIdAsync(int id)
        {
            return await _context.Borrows
                .Where(b => b.BorrowID == id)
                .Select(b => new BorrowDto
                {
                    BorrowID = b.BorrowID,
                    BookID = b.BookID,
                    MemberID = b.MemberID,
                    LibrarianID = b.LibrarianID,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<BorrowDto> CreateAsync(CreateBorrowDto dto)
        {
            var book = await _context.Books.FindAsync(dto.BookID);

            if (book == null)
                throw new Exception("Book not found.");

            if (!book.Availability)
                throw new Exception("Book is not available.");

            var borrow = new Borrow
            {
                BookID = dto.BookID,
                MemberID = dto.MemberID,
                LibrarianID = dto.LibrarianID,
                BorrowDate = dto.BorrowDate,
                DueDate = dto.DueDate,
                Status = dto.Status
            };

            book.Availability = false;

            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();

            return new BorrowDto
            {
                BorrowID = borrow.BorrowID,
                BookID = borrow.BookID,
                MemberID = borrow.MemberID,
                LibrarianID = borrow.LibrarianID,
                BorrowDate = borrow.BorrowDate,
                DueDate = borrow.DueDate,
                ReturnDate = borrow.ReturnDate,
                Status = borrow.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBorrowDto dto)
        {
            var borrow = await _context.Borrows.FindAsync(id);

            if (borrow == null)
                return false;

            borrow.DueDate = dto.DueDate;
            borrow.ReturnDate = dto.ReturnDate;
            borrow.Status = dto.Status;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReturnBookAsync(int borrowId)
        {
            var borrow = await _context.Borrows
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BorrowID == borrowId);

            if (borrow == null)
                return false;

            borrow.ReturnDate = DateTime.UtcNow;
            borrow.Status = "Returned";
            borrow.Book.Availability = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BorrowDto>> GetBorrowingHistoryAsync(int memberId)
        {
            return await _context.Borrows
                .Where(b => b.MemberID == memberId)
                .Select(b => new BorrowDto
                {
                    BorrowID = b.BorrowID,
                    BookID = b.BookID,
                    MemberID = b.MemberID,
                    LibrarianID = b.LibrarianID,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<BorrowDto>> SearchAsync(
    int? bookId,
    int? memberId,
    string? status)
        {
            var query = _context.Borrows.AsQueryable();

            if (bookId.HasValue)
            {
                query = query.Where(b => b.BookID == bookId.Value);
            }

            if (memberId.HasValue)
            {
                query = query.Where(b => b.MemberID == memberId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(b => b.Status == status);
            }

            return await query
                .Select(b => new BorrowDto
                {
                    BorrowID = b.BorrowID,
                    BookID = b.BookID,
                    MemberID = b.MemberID,
                    LibrarianID = b.LibrarianID,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                })
                .ToListAsync();
        }
    }
}
