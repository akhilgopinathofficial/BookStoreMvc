using BookStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Configuration
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Shelve> Shelves { get; set; }
        public DbSet<ShelveSPData> ShelveSPData { get; set; }
        public DbSet<BookSP> BookSP { get; set; }
        public DbSet<RackSPData> RackSPData { get; set; }
    }
}
