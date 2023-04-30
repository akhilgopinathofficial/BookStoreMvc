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
    public class ShelveRackService : IShelveRackService
    {
        private readonly IMapper _mapper;
        private readonly IShelveRackRepository _shelveRackRepository;
        public ShelveRackService(IMapper mapper, IShelveRackRepository shelveRackRepository)
        {
            _mapper = mapper;
            _shelveRackRepository = shelveRackRepository;
        }

        public async Task<List<ShelveDto>> GetAllShelves() 
        {
            try
            {
                var shelves = await _shelveRackRepository.GetAllShelves();
                var dtoList = _mapper.Map<List<ShelveDto>>(shelves);
                return dtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<List<RackDto>> GetAllRacks()
        {
            try
            {
                var shelves = await _shelveRackRepository.GetAllRacks();
                var dtoList = _mapper.Map<List<RackDto>>(shelves);
                return dtoList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Delete shelve information of specific id provided
        /// </summary>
        /// <param name="id">Id of shelve to delete</param>
        /// <returns></returns>
        public async Task DeleteShelve(int id)
        {
            await _shelveRackRepository.DeleteShelve(id);
        }

        public async Task AddShelve(ShelveDto shelveDto)
        {
            try
            {
                var shelve = _mapper.Map<Shelve>(shelveDto);
                await _shelveRackRepository.AddShelve(shelve);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
