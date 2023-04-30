using BookStore.BL.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Creates book information
        /// </summary>
        /// <param name="bookDto">dto model to create book</param>
        /// <returns></returns>
        Task AddBook(BookDto bookDto);

        /// <summary>
        /// Get list of all books
        /// </summary>
        /// <param name=""></param>
        /// <returns>return list of view model with books</returns>
        Task<List<BookDto>> GetAllBooks(bool filterDeleted);

        /// <summary>
        /// Get book by Id
        /// </summary>
        /// <param name="id">id of book detail to edit</param>
        /// <returns>Returns book</returns>
        Task<BookDto> GetBookById(int id);

        /// <summary>
        /// Delete book information of specific id provided
        /// </summary>
        /// <param name="id">Id of book to delete</param>
        /// <returns></returns>
        Task DeleteBook(int id);
    }
}
