using AutoMapper;
using Business.Enums;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public class SinifProfile : Profile
    {
        public SinifProfile()
        {
            CreateMap<Sinif, SinifQueryModel>()
                .ForMember(d => d.OgrenciSayisi, o => o.MapFrom(s => s.Ogrenci.Count))
                .ForMember(d => d.Ogrenciler, o => o.MapFrom(s => s.Ogrenci.Select(ogrenci => new OgrenciQueryModel()
                {
                    AdiSoyadi = ogrenci.Adi + " " + ogrenci.Soyadi,
                    Cinsiyeti = (Cinsiyetler)ogrenci.Cinsiyeti,
                    DogumTarihi = ogrenci.DogumTarihi.ToString("dd.MM.yyyy"),
                    Guid = ogrenci.Guid,
                    Id = ogrenci.Id,
                    MezunMu = ogrenci.MezunMu ? "Mezun" : "Devam",
                    Sinif = ogrenci.Sinif.Adi,
                    TcKimlikNo = ogrenci.TcKimlikNo,
                    Uyruk = (Uyruklar)ogrenci.Uyruk
                })));
        }
    }
}
