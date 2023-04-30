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
    public class ShelveRackRepository : IShelveRackRepository
    {
        protected readonly BookContext _context;
        public ShelveRackRepository(BookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates book information
        /// </summary>
        /// <param name="bookDto">dto model to create book</param>
        /// <returns></returns>
        public async Task AddShelve(Shelve shelve)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@code",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = shelve.Code ?? (object) DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@name",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = shelve.Name ?? (object) DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@rackid",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = shelve.RackId
                        }};
                await _context.Database.ExecuteSqlRawAsync("SP_AddShelve @code, @name, @rackid;", param);
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<ShelveSPData>> GetAllShelves()
        {
            try
            {
                var list = await _context.ShelveSPData.FromSqlRaw("EXEC SP_GetShelveData").ToListAsync();
                return list.Where(x=>!x.IsDeleted).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RackSPData>> GetAllRacks()
        {
            try
            {
                var list = await _context.RackSPData.FromSqlRaw("EXEC SP_GetRackData").ToListAsync();
                return list.Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task DeleteShelve(int id)
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
                await _context.Database.ExecuteSqlRawAsync("SP_DeleteShelve @id", param);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
