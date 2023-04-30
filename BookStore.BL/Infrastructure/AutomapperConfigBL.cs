using AutoMapper;
using BookStore.BL.BusinessModels;
using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.BL.Infrastructure
{
    public class AutomapperConfigBL : Profile
    {
        public AutomapperConfigBL()
        {
            CreateMap<Base, BaseDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<ShelveSPData, BaseDto>();
            CreateMap<ShelveSPData, ShelveDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.RackId, opts => opts.MapFrom(src => src.RackId))
            .ForMember(dest => dest.IsDeleted, opts => opts.MapFrom(src => src.IsDeleted)).ForAllOtherMembers(m => m.Ignore());

            CreateMap<BookSP, BookDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsAvailable, opts => opts.MapFrom(src => src.IsAvailable))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
            .ForMember(dest => dest.ShelfId, opts => opts.MapFrom(src => src.ShelfId))
            .ForMember(dest => dest.ShelfName, opts => opts.MapFrom(src => src.ShelfName))
            .ForMember(dest => dest.IsDeleted, opts => opts.MapFrom(src => src.IsDeleted)).ForAllOtherMembers(m => m.Ignore());

            CreateMap<ShelveDto, Shelve>().ReverseMap();

            CreateMap<RackSPData, BaseDto>();
            CreateMap<RackSPData, RackDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsDeleted, opts => opts.MapFrom(src => src.IsDeleted)).ForAllOtherMembers(m => m.Ignore());
        }
    }
}
