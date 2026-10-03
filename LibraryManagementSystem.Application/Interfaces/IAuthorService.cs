using LibraryManagementSystem.Application.DTOs.Author;
using LibraryManagementSystem.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();

        Task<AuthorDto?> GetByIdAsync(int id);

        Task<AuthorDto> CreateAsync(CreateAuthorDto dto);

        Task<bool> UpdateAsync(int id, UpdateAuthorDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId);
    }
}
