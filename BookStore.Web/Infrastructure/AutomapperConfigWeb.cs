using AutoMapper;
using BookStore.BL.BusinessModels;
using BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Infrastructure
{
    public class AutomapperConfigWeb : Profile
    {
        public AutomapperConfigWeb()
        {
            CreateMap<BaseViewModel, BaseDto>();
            CreateMap<BookViewModel, BookDto>();
            CreateMap<BookDto, BookViewModel>();
            CreateMap<ShelveDto, ShelveViewModel>().ReverseMap();
            CreateMap<ShelveViewModel, ShelveDto>().ReverseMap();
            CreateMap<RackDto, RackViewModel>().ReverseMap();
        }
    }
}
