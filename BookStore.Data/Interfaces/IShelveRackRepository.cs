using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interfaces
{
    public interface IShelveRackRepository
    {

        /// <summary>
        /// Creates book information
        /// </summary>
        /// <param name="bookDto">dto model to create book</param>
        /// <returns></returns>
        Task AddShelve(Shelve shelve);
        Task<List<ShelveSPData>> GetAllShelves();

        Task DeleteShelve(int id);

        Task<List<RackSPData>> GetAllRacks();
    }
}
