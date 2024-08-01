using AutoMapper;
using Business.Enums;
using Business.Models.Reports;
using DataAccess.Entities;

namespace Business.Services.Reports
{
    public class OgrenciRaporuProfile : Profile
    {
        public OgrenciRaporuProfile()
        {
            CreateMap<OgrenciRaporuView, OgrenciRaporuQueryModel>()
                .ForMember(d => d.OgrenciDogumTarihi, o => o.MapFrom(s => s.OgrenciDogumTarihi.HasValue ? s.OgrenciDogumTarihi.Value.ToShortDateString() : string.Empty))
                .ForMember(d => d.OgrenciMezunMu, o => o.MapFrom(s => s.OgrenciMezuniyet.HasValue ? s.OgrenciMezuniyet.Value ? "Mezun" : "Devam" : string.Empty))
                .ForMember(d => d.OgrenciUyruk, o => o.MapFrom(s => s.OgrenciUyruk.HasValue ? ((Uyruklar)s.OgrenciUyruk.Value).ToString() : string.Empty))
                .ForMember(d => d.OgrenciCinsiyeti, o => o.MapFrom(s => s.OgrenciCinsiyet.HasValue ? ((Cinsiyetler)s.OgrenciCinsiyet.Value).ToString() : string.Empty))
                .ForMember(d => d.DersOrtalamasi, o => o.MapFrom(s => s.DersOrtalamasi.HasValue ? s.DersOrtalamasi.Value.ToString("N1") : string.Empty));
        }
    }
}
