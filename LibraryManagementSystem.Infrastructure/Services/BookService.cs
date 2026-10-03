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
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return await _context.Books
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

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => b.BookID == id)
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
                .FirstOrDefaultAsync();
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Genre = dto.Genre,
                Language = dto.Language,
                Availability = dto.Availability,
                AuthorID = dto.AuthorID,
                CategoryID = dto.CategoryID
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookDto
            {
                BookID = book.BookID,
                ISBN = book.ISBN,
                Title = book.Title,
                Genre = book.Genre,
                Language = book.Language,
                Availability = book.Availability,
                AuthorID = book.AuthorID,
                CategoryID = book.CategoryID
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return false;

            book.ISBN = dto.ISBN;
            book.Title = dto.Title;
            book.Genre = dto.Genre;
            book.Language = dto.Language;
            book.Availability = dto.Availability;
            book.AuthorID = dto.AuthorID;
            book.CategoryID = dto.CategoryID;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return false;

            var hasBorrows = await _context.Borrows
                .AnyAsync(b => b.BookID == id);

            if (hasBorrows)
                throw new InvalidOperationException(
                    "This book cannot be deleted because it has borrowing records.");

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BookDto>> SearchByTitleAsync(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title))
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

        public async Task<BookDto?> SearchByISBNAsync(string isbn)
        {
            return await _context.Books
                .Where(b => b.ISBN == isbn)
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
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookDto>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Where(b => b.CategoryID == categoryId)
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

        public async Task<IEnumerable<BookDto>> GetByAuthorAsync(int authorId)
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

        public async Task<bool> CheckAvailabilityAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return false;

            return book.Availability;
        }
    }
}