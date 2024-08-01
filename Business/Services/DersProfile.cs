using AutoMapper;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public class DersProfile : Profile
    {
        public DersProfile()
        {
            CreateMap<Ders, DersQueryModel>()
                .ForMember(d => d.OgrenciSayisi, o => o.MapFrom(s => s.OgrenciDers.Count));
        }
    }
}
