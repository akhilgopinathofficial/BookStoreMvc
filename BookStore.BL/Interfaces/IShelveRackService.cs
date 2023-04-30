using BookStore.BL.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Interfaces
{
    public interface IShelveRackService
    {
        Task<List<ShelveDto>> GetAllShelves();
        Task AddShelve(ShelveDto shelveDto);
        Task DeleteShelve(int id);
        Task<List<RackDto>> GetAllRacks();
    }
}
