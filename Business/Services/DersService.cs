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
    public class DersService : ServiceBase<Ders, DersQueryModel, DersCommandModel>
	{
		public DersService(RepoBase<Ders> repo, UnitOfWorkBase unitOfWork, MapperBase<Ders, DersQueryModel, DersCommandModel> mapper) : base(repo, unitOfWork, mapper)
		{
			Set(new DersProfile());
		}

        public override async Task<Response<DersCommandModel>> Create(DersCommandModel commandModel, CancellationToken cancellationToken = default)
        {
			//if (await QueryCommand().AnyAsync(q => EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim(), cancellationToken))
            if (await QueryCommand().AnyAsync(q => Db.EsitMi(q.Adi, commandModel.Adi).Value, cancellationToken))
				return new ErrorResponse<DersCommandModel>("Girilen ders adına sahip kayıt bulunmaktadır!");
            return await base.Create(commandModel, cancellationToken);
        }

        public override async Task<Response<DersCommandModel>> Update(DersCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            //if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id && 
            //    EF.Functions.Collate(q.Adi, _repo.Collation).ToUpper() == EF.Functions.Collate(commandModel.Adi, _repo.Collation).ToUpper().Trim(), cancellationToken))
            if (await QueryCommand().AnyAsync(q => q.Id != commandModel.Id && Db.EsitMi(q.Adi, commandModel.Adi).Value, cancellationToken))
                return new ErrorResponse<DersCommandModel>("Girilen ders adına sahip kayıt bulunmaktadır!");
            return await base.Update(commandModel, cancellationToken);
        }
    }
}
