using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using N4CoreLite.Extensions;
using N4CoreLite.Mappers.Bases;
using N4CoreLite.Repositories.Bases;
using N4CoreLite.Responses;
using N4CoreLite.Responses.Bases;
using N4CoreLite.Services.Bases;

namespace Business.Services
{
    public class OgrenciService : ServiceBase<Ogrenci, OgrenciQueryModel, OgrenciCommandModel>
    {
        private readonly RepoBase<OgrenciDers> _ogrenciDersRepo;
        private readonly IDbProcedures _dbProcedures;

        public OgrenciService(RepoBase<Ogrenci> repo, UnitOfWorkBase unitOfWork, MapperBase<Ogrenci, OgrenciQueryModel, OgrenciCommandModel> mapper,
            RepoBase<OgrenciDers> ogrenciDersRepo, IDbProcedures dbProcedures) : base(repo, unitOfWork, mapper)
        {
            Set(new OgrenciProfile());
            _ogrenciDersRepo = ogrenciDersRepo;
            _dbProcedures = dbProcedures;
        }

        public override IQueryable<OgrenciQueryModel> Query(bool noEntityTracking = true)
        {
            return base.Query(noEntityTracking).OrderBy(q => q.Sinif).ThenBy(q => q.AdiSoyadi);
        }

        public override async Task<Response<OgrenciQueryModel>> GetItem(int id, CancellationToken cancellationToken = default)
        {
            var ogrenciResponse = await base.GetItem(id, cancellationToken);
            if (ogrenciResponse.IsSuccessful)
                ogrenciResponse.Data.NotOrtalamasi = await NotOrtalamasiGetir(ogrenciResponse.Data.Id);
            return ogrenciResponse;
        }

        private async Task<string> NotOrtalamasiGetir(int ogrenciId)
        {
            OutputParameter<decimal?> notOrtalamasi = new OutputParameter<decimal?>();
            await _dbProcedures.NotOrtalamaHesaplaAsync(ogrenciId, notOrtalamasi);
            if (notOrtalamasi is not null && notOrtalamasi.Value.HasValue)
                return notOrtalamasi.Value.Value.ToString("N1");
            return string.Empty;
        }

        public override async Task<Response<OgrenciCommandModel>> Create(OgrenciCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            //if (await QueryCommand().AnyAsync(q => EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim() &&
            //    EF.Functions.Collate(q.Soyadi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Soyadi, _repo.Collation).ToUpper().Trim() &&
            //    q.DogumTarihi == commandModel.DogumTarihi, cancellationToken))
            if (await QueryCommand().AnyAsync(q => Db.EsitMi(q.Adi, commandModel.Adi).Value && Db.EsitMi(q.Soyadi, commandModel.Soyadi).Value && q.DogumTarihi == commandModel.DogumTarihi, cancellationToken))
                return new ErrorResponse<OgrenciCommandModel>("Girilen öğrenci ad ve soyadi ile doğum tarihine sahip kayıt bulunmaktadır!");
            commandModel.KullaniciAdi = $"{commandModel.Adi}@{commandModel.Soyadi}.com";
            commandModel.KullaniciAdi = commandModel.KullaniciAdi.ChangeTurkishCharactersToEnglish();
            return await base.Create(commandModel, cancellationToken);
        }

        public override async Task<Response<OgrenciCommandModel>> Update(OgrenciCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            //if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id &&
            //    EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim() &&
            //    EF.Functions.Collate(q.Soyadi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Soyadi, _repo.Collation).ToUpper().Trim() &&
            //    q.DogumTarihi == commandModel.DogumTarihi, cancellationToken))
            if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id && Db.EsitMi(q.Adi, commandModel.Adi).Value && Db.EsitMi(q.Soyadi, commandModel.Soyadi).Value && q.DogumTarihi == commandModel.DogumTarihi, cancellationToken))
                return new ErrorResponse<OgrenciCommandModel>("Girilen öğrenci ad ve soyadi ile doğum tarihine sahip kayıt bulunmaktadır!");
            commandModel.KullaniciAdi = $"{commandModel.Adi}@{commandModel.Soyadi}.com";
            commandModel.KullaniciAdi = commandModel.KullaniciAdi.ChangeTurkishCharactersToEnglish();
            _ogrenciDersRepo.Delete(od => od.OgrenciId == commandModel.Id);
            return await base.Update(commandModel, cancellationToken);
        }

        public override async Task<Response<OgrenciCommandModel>> Delete(int id, CancellationToken cancellationToken = default)
        {
            _ogrenciDersRepo.Delete(od => od.OgrenciId == id);
            return await base.Delete(id, cancellationToken);
        }
    }
}
