using AutoMapper;
using BookStore.BL.BusinessModels;
using BookStore.BL.Interfaces;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class ShelveRackController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IShelveRackService _shelveRackService;
        public IActionResult Index()
        {
            return View();
        }

        public ShelveRackController(IMapper mapper, IShelveRackService shelveRackService)
        {
            _mapper = mapper;
            _shelveRackService = shelveRackService;
        }

        public IActionResult Shelve()
        {
            return View();
        }

        /// <summary>
        /// Creates shelve information
        /// </summary>
        /// <param name="vmShelve">view model to create shelve</param>
        /// <returns>Return created object and redirect to book list</returns>
        [HttpPost]
        public async Task<ActionResult> CreateUpdate(ShelveViewModel vmShelve)
        {
            try
            {
                var shelveDto = _mapper.Map<ShelveDto>(vmShelve);
                await _shelveRackService.AddShelve(shelveDto);
                return Json(new { Message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { Message = "failed" });
            }
        }


        /// <summary>
        /// Get list of all shelves
        /// </summary>
        /// <param name=""></param>
        /// <returns>return list of view model with shelves</returns>
        public async Task<ActionResult> GetShelveList()
        {
            try
            {
                var result = await _shelveRackService.GetAllShelves();
                var dtoList = _mapper.Map<List<ShelveViewModel>>(result);
                return Json(new { Data = dtoList, Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Data = "", Message = "failed" });
            }
        }

        /// <summary>
        /// Delete shelve information of specific id provided
        /// </summary>
        /// <param name="id">Id of shelve to delete</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _shelveRackService.DeleteShelve(id);
                return Json(new { Data = "", Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "failed" });
            }
        }

        /// <summary>
        /// Get Rack List for dropdown
        /// </summary>
        /// <returns>Returns Rack list</returns>
        public async Task<ActionResult> GetRackList()
        {
            try
            {
                var result = await _shelveRackService.GetAllRacks();
                var dtoList = _mapper.Map<List<RackViewModel>>(result);
                return Json(new { Data = dtoList, Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Data = "", Message = "failed" });
            }
        }
    }
}
