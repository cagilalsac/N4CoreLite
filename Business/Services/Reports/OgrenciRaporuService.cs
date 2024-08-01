using Business.Enums;
using Business.Models.Reports;
using Business.Services.Reports.Bases;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using N4CoreLite.Extensions;
using N4CoreLite.Mappers.Bases;
using N4CoreLite.Repositories.Bases;
using N4CoreLite.Responses;
using N4CoreLite.Responses.Bases;
using N4CoreLite.Services.Bases;

namespace Business.Services.Reports
{
    public class OgrenciRaporuService : ServiceBase<OgrenciRaporuView, OgrenciRaporuQueryModel, OgrenciRaporuCommandModel>, IOgrenciRaporuService
    {
        private readonly RepoBase<Sinif> _sinifRepo;
        private readonly RepoBase<Ders> _dersRepo;

        public OgrenciRaporuService(RepoBase<OgrenciRaporuView> repo, UnitOfWorkBase unitOfWork, MapperBase<OgrenciRaporuView, OgrenciRaporuQueryModel, OgrenciRaporuCommandModel> mapper,
            RepoBase<Sinif> sinifRepo, RepoBase<Ders> dersRepo) : base(repo, unitOfWork, mapper)
        {
            _sinifRepo = sinifRepo;
            _dersRepo = dersRepo;
            Set(new OgrenciRaporuProfile());
        }

        public async Task<Response<OgrenciRaporuViewModel>> Getir(OgrenciRaporuViewModel viewModel = null, CancellationToken cancellationToken = default)
        {
            var entityQuery = _repo.Query(true);
            if (viewModel is not null)
            {
                entityQuery = entityQuery.OrderBy(viewModel);
                if (!string.IsNullOrWhiteSpace(viewModel.Command.OgrenciAdiSoyadi))
                    entityQuery = entityQuery.Where(s => EF.Functions.Collate(s.OgrenciAdiSoyadi, _repo.Collation).ToUpper().Contains(EF.Functions.Collate(viewModel.Command.OgrenciAdiSoyadi, _repo.Collation).ToUpper().Trim()));
                if (viewModel.Command.OgrenciDogumTarihiBaslangic.HasValue)
                    entityQuery = entityQuery.Where(q => q.OgrenciDogumTarihi >= viewModel.Command.OgrenciDogumTarihiBaslangic);
                if (viewModel.Command.OgrenciDogumTarihiBitis.HasValue)
                    entityQuery = entityQuery.Where(q => q.OgrenciDogumTarihi <= viewModel.Command.OgrenciDogumTarihiBitis);
                if (viewModel.Command.OgrenciMezunMu.HasValue)
                    entityQuery = entityQuery.Where(q => q.OgrenciMezuniyet == viewModel.Command.OgrenciMezunMu);
                if (viewModel.Command.OgrenciUyruk.HasValue)
                    entityQuery = entityQuery.Where(q => q.OgrenciUyruk == viewModel.Command.OgrenciUyruk);
                if (viewModel.Command.SinifId.HasValue)
                    entityQuery = entityQuery.Where(q => q.SinifId == viewModel.Command.SinifId);
                if (viewModel.Command.DersIdleri is not null && viewModel.Command.DersIdleri.Any())
                    entityQuery = entityQuery.Where(s => viewModel.Command.DersIdleri.Contains(s.DersId ?? 0));
            }
            else
            {
                viewModel = new OgrenciRaporuViewModel();
                viewModel.MezunDictionary = new Dictionary<string, string>()
                {
                    { "Mezun", "true" },
                    { "Devam", "false" }
                };
                viewModel.UyrukDictionary = new Dictionary<string, string>()
                {
                    { Uyruklar.TC.ToString(), Convert.ToString((int)Uyruklar.TC) },
                    { Uyruklar.Yabancı.ToString(), Convert.ToString((int)Uyruklar.Yabancı) }
                };
                List<Sinif> sinifList = await _sinifRepo.Query(true).OrderBy(q => q.Adi).ToListAsync(cancellationToken);
                List<Ders> dersList = await _dersRepo.Query(true).OrderBy(d => d.Adi).ToListAsync(cancellationToken);
                viewModel.SinifDictionary = sinifList.ToDictionary(s => s.Id.ToString(), s => s.Adi);
                viewModel.DersDictionary = dersList.ToDictionary(d => d.Id.ToString(), d => d.Adi);
            }
            viewModel.OrderExpressionsForEntityProperties = new List<string>()
            {
                "Öğrenci Adı Soyadı",
                "Öğrenci Mezuniyet",
                "Sınıf",
                "Ders Ortalaması"
            };
            var query = Query(entityQuery).Paginate(viewModel);
            viewModel.List = await query.ToListAsync(cancellationToken);
            viewModel.Message = viewModel.TotalRecordsCount > 0 ? viewModel.TotalRecordsCount + " kayıt bulundu." : "Kayıt bulunamadı!";
            return new SuccessResponse<OgrenciRaporuViewModel>(viewModel);
        }
    }
}
