using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.DTOs.Book;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

        Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
    }
}