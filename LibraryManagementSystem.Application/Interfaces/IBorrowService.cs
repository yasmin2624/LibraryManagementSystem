using LibraryManagementSystem.Application.DTOs.Borrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowDto>> GetAllAsync();

        Task<BorrowDto?> GetByIdAsync(int id);

        Task<BorrowDto> CreateAsync(CreateBorrowDto dto);

        Task<bool> UpdateAsync(int id, UpdateBorrowDto dto);

        Task<bool> ReturnBookAsync(int borrowId);

        Task<IEnumerable<BorrowDto>> GetBorrowingHistoryAsync(int memberId);

        Task<IEnumerable<BorrowDto>> SearchAsync(
          int? bookId,
          int? memberId,
          string? status);

    }
}

