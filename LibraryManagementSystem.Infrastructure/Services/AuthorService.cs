using LibraryManagementSystem.Application.DTOs.Author;
using LibraryManagementSystem.Application.DTOs.Book;
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
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDto
                {
                    AuthorID = a.AuthorID,
                    Name = a.Name,
                    Email = a.Email,
                    Nationality = a.Nationality
                })
                .ToListAsync();
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Where(a => a.AuthorID == id)
                .Select(a => new AuthorDto
                {
                    AuthorID = a.AuthorID,
                    Name = a.Name,
                    Email = a.Email,
                    Nationality = a.Nationality
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                Email = dto.Email,
                Nationality = dto.Nationality
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                AuthorID = author.AuthorID,
                Name = author.Name,
                Email = author.Email,
                Nationality = author.Nationality
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateAuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return false;

            author.Name = dto.Name;
            author.Email = dto.Email;
            author.Nationality = dto.Nationality;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return false;

            var hasBooks = await _context.Books
                .AnyAsync(b => b.AuthorID == id);

            if (hasBooks)
                throw new InvalidOperationException(
                    "This author cannot be deleted because they have books assigned to them.");

            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorID == authorId)
                .Select(b => new BookDto
                {
                    BookID = b.BookID,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Genre = b.Genre,
                    Language = b.Language,
                    Availability = b.Availability,
                    AuthorID = b.AuthorID,
                    CategoryID = b.CategoryID
                })
                .ToListAsync();
        }
    }
}
