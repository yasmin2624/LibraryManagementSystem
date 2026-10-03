using LibraryManagementSystem.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces
{

    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();

        Task<BookDto?> GetByIdAsync(int id);

        Task<BookDto> CreateAsync(CreateBookDto dto);

        Task<bool> UpdateAsync(int id, UpdateBookDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BookDto>> SearchByTitleAsync(string title);

        Task<BookDto?> SearchByISBNAsync(string isbn);

        Task<IEnumerable<BookDto>> GetByCategoryAsync(int categoryId);

        Task<IEnumerable<BookDto>> GetByAuthorAsync(int authorId);

        Task<bool> CheckAvailabilityAsync(int id);
    }
}
