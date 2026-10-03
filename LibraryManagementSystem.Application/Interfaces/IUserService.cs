using LibraryManagementSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto> CreateAsync(CreateUserDto dto);

        Task<bool> UpdateAsync(int id, UpdateUserDto dto);

        Task<bool> DeleteAsync(int id);

        Task<UserDto?> GetProfileAsync(int id);
    }
}
