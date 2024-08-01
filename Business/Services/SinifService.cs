using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using N4CoreLite.Mappers.Bases;
using N4CoreLite.Repositories.Bases;
using N4CoreLite.Responses;
using N4CoreLite.Responses.Bases;
using N4CoreLite.Services.Bases;

namespace Business.Services
{
    public class SinifService : ServiceBase<Sinif, SinifQueryModel, SinifCommandModel>
    {
        public SinifService(RepoBase<Sinif> repo, UnitOfWorkBase unitOfWork, MapperBase<Sinif, SinifQueryModel, SinifCommandModel> mapper,
            RepoBase<Ogrenci> ogrenciRepo) : base(repo, unitOfWork, mapper)
        {
            Set(new SinifProfile());
        }

        public override async Task<Response<SinifCommandModel>> Create(SinifCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            //if (await QueryCommand().AnyAsync(q => EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim(), cancellationToken))
            if (await QueryCommand().AnyAsync(q => Db.EsitMi(q.Adi, commandModel.Adi).Value, cancellationToken))
                return new ErrorResponse<SinifCommandModel>("Girilen sınıf adına sahip kayıt bulunmaktadır!");
            return await base.Create(commandModel, cancellationToken);
        }

        public override async Task<Response<SinifCommandModel>> Update(SinifCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            //if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id && 
            //    EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim(), cancellationToken))
            if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id && Db.EsitMi(q.Adi, commandModel.Adi).Value, cancellationToken))
                return new ErrorResponse<SinifCommandModel>("Girilen sınıf adına sahip kayıt bulunmaktadır!");
            return await base.Update(commandModel, cancellationToken);
        }

        public override async Task<Response<SinifCommandModel>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var sinifResponse = await GetItem(id, cancellationToken);
            if (sinifResponse.IsSuccessful && sinifResponse.Data.OgrenciSayisi > 0)
                return new ErrorResponse<SinifCommandModel>("Sınıfta öğrenci bulunduğundan sınıf silinemez!");
            return await base.Delete(id, cancellationToken);
        }
    }
}
