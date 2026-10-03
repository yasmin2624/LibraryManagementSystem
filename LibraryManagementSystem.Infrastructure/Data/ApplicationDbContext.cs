using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Role -> Users
            // One Role has many Users
            // =========================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Author -> Books
            // One Author has many Books
            // =========================
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Category -> Books
            // One Category has many Books
            // =========================
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Book -> Borrows
            // One Book has many Borrow records
            // =========================
            modelBuilder.Entity<Borrow>()
                .HasOne(br => br.Book)
                .WithMany(b => b.Borrows)
                .HasForeignKey(br => br.BookID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // User (Member) -> Borrows
            // One Member has many Borrow records
            // =========================
            modelBuilder.Entity<Borrow>()
                .HasOne(br => br.Member)
                .WithMany(u => u.MemberBorrows)
                .HasForeignKey(br => br.MemberID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // User (Librarian) -> Borrows
            // One Librarian has many Borrow records
            // =========================
            modelBuilder.Entity<Borrow>()
                .HasOne(br => br.Librarian)
                .WithMany(u => u.LibrarianBorrows)
                .HasForeignKey(br => br.LibrarianID)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Borrow -> Payments
            // One Borrow has many Payments
            // =========================
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Borrow)
                .WithMany(br => br.Payments)
                .HasForeignKey(p => p.BorrowID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
               .Property(p => p.Amount)
               .HasPrecision(18, 2);

        }
    }
}


