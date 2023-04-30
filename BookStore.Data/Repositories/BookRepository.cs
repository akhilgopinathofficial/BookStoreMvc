using BookStore.Data.Configuration;
using BookStore.Data.Interfaces;
using BookStore.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        protected readonly BookContext _context;
        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.Id
                        },
                        new SqlParameter() {
                            ParameterName = "@code",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.Code ?? (object) DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@name",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.Name ?? (object) DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@isavailable",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.IsAvailable
                        },
                         new SqlParameter() {
                            ParameterName = "@price",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.Price
                        },
                        new SqlParameter() {
                            ParameterName = "@shelfid",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = book.ShelfId
                        }};
                await _context.Database.ExecuteSqlRawAsync("SP_AddUpdateBook @id, @code, @name, @isavailable, @price, @shelfid;", param);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<BookSP>> GetAllBooks(bool filterDeleted)
        {
            try
            {
                var bookList = await _context.BookSP.FromSqlRaw("EXEC SP_GetAllBookList").ToListAsync();
                if (!filterDeleted)
                    return bookList.Where(x => !x.IsDeleted).ToList();
                return bookList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<BookSP> GetBookById(int id)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                                    ParameterName = "@id",
                                    SqlDbType = System.Data.SqlDbType.Int,
                                    Direction = System.Data.ParameterDirection.Input,
                                    Value = id
                                }};
            var book = await _context.BookSP.FromSqlRaw("EXEC SP_GetBook @id", param).ToListAsync();
            return book.FirstOrDefault();
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public async Task DeleteBook(int id)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                                    ParameterName = "@id",
                                    SqlDbType = System.Data.SqlDbType.Int,
                                    Direction = System.Data.ParameterDirection.Input,
                                    Value = id
                                }};
                await _context.Database.ExecuteSqlRawAsync("SP_DeleteBook @id", param);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
