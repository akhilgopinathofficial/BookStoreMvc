using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interfaces
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task<List<BookSP>> GetAllBooks(bool filterDeleted);
        Task<BookSP> GetBookById(int id);
        Task DeleteBook(int id);
    }
}
