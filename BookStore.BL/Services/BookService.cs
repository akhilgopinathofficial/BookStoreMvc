using AutoMapper;
using BookStore.BL.BusinessModels;
using BookStore.BL.Interfaces;
using BookStore.Data.Interfaces;
using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        public BookService(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Creates book information
        /// </summary>
        /// <param name="bookDto">dto model to create book</param>
        /// <returns></returns>
        public async Task AddBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddBook(book);
        }

        /// <summary>
        /// Get list of all books
        /// </summary>
        /// <param name=""></param>
        /// <returns>return list of view model with books</returns>
        public async Task<List<BookDto>> GetAllBooks(bool filterDeleted)
        {
            var list = await _bookRepository.GetAllBooks(filterDeleted);
            var dtoList = _mapper.Map<List<BookDto>>(list);
            return dtoList;
        }

        /// <summary>
        /// Get book by Id
        /// </summary>
        /// <param name="id">id of book detail to edit</param>
        /// <returns>Returns book</returns>
        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            var dto = _mapper.Map<BookDto>(book);
            return dto;
        }

        /// <summary>
        /// Delete book information of specific id provided
        /// </summary>
        /// <param name="id">Id of book to delete</param>
        /// <returns></returns>
        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }
    }
}
