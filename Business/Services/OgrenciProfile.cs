using AutoMapper;
using Business.Enums;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public class OgrenciProfile : Profile
    {
        public OgrenciProfile()
        {
            CreateMap<Ogrenci, OgrenciQueryModel>()
                .ForMember(d => d.AdiSoyadi, o => o.MapFrom(s => s.Adi + " " + s.Soyadi))
                .ForMember(d => d.Sinif, o => o.MapFrom(s => s.Sinif.Adi))
                .ForMember(d => d.DogumTarihi, o => o.MapFrom(s => s.DogumTarihi.ToString("dd.MM.yyyy")))
                .ForMember(d => d.MezunMu, o => o.MapFrom(s => s.MezunMu ? "Mezun" : "Devam"))
                .ForMember(d => d.Uyruk, o => o.MapFrom(s => (Uyruklar)s.Uyruk))
                .ForMember(d => d.Cinsiyeti, o => o.MapFrom(s => (Cinsiyetler)s.Cinsiyeti))
                .ForMember(d => d.Dersleri, o => o.MapFrom(s => string.Join("<br />", s.OgrenciDers.Select(ogrenciDers => ogrenciDers.Ders.Adi))));
            CreateMap<Ogrenci, OgrenciCommandModel>()
                .ForMember(d => d.DersIdleri, o => o.MapFrom(s => s.OgrenciDers.Select(ogrenciDers => ogrenciDers.DersId)));
            CreateMap<OgrenciCommandModel, Ogrenci>()
                .ForMember(d => d.OgrenciDers, o => o.MapFrom(s => s.DersIdleri.Select(dersId => new OgrenciDers() { DersId = dersId })));
        }
    }
}
