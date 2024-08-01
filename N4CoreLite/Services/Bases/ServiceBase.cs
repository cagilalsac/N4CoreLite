#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using N4CoreLite.Extensions;
using N4CoreLite.Mappers.Bases;
using N4CoreLite.Models;
using N4CoreLite.Records.Bases;
using N4CoreLite.Repositories.Bases;
using N4CoreLite.Responses;
using N4CoreLite.Responses.Bases;

namespace N4CoreLite.Services.Bases
{
    public abstract class ServiceBase<TEntity, TQueryModel, TCommandModel> : IService<TEntity, TQueryModel, TCommandModel>
        where TEntity : Record, new() where TQueryModel : Record, new() where TCommandModel : Record, new()
    {
        protected readonly RepoBase<TEntity> _repo;
        protected readonly UnitOfWorkBase _unitOfWork;
        protected readonly MapperBase<TEntity, TQueryModel, TCommandModel> _mapper;

        protected ServiceBase(RepoBase<TEntity> repo, UnitOfWorkBase unitOfWork, MapperBase<TEntity, TQueryModel, TCommandModel> mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Set(params Profile[] mapperProfiles) => _mapper.Set(mapperProfiles);

        public virtual IQueryable<TQueryModel> Query(bool noEntityTracking = true)
        {
            return _repo.Query(noEntityTracking).ProjectTo<TQueryModel>(_mapper.Configuration);
        }

        public virtual IQueryable<TQueryModel> Query(IQueryable<TEntity> entityQuery)
        {
            return entityQuery.ProjectTo<TQueryModel>(_mapper.Configuration);
        }

        public virtual IQueryable<TCommandModel> QueryCommand()
        {
            return _repo.Query(false).ProjectTo<TCommandModel>(_mapper.Configuration);
        }

        public virtual async Task<Response<List<TQueryModel>>> GetList(CancellationToken cancellationToken = default)
        {
            var list = await Query().ToListAsync(cancellationToken);
            return list.Any() ? new SuccessResponse<List<TQueryModel>>(list.Count + " kayıt bulundu.", list) : new ErrorResponse<List<TQueryModel>>("Kayıt bulunamadı!");
        }

        public virtual async Task<Response<List<TQueryModel>>> GetList(PageOrderModel pageOrderModel, CancellationToken cancellationToken = default)
        {
            var entityQuery = _repo.Query(true);
            if (!string.IsNullOrWhiteSpace(pageOrderModel.OrderExpression))
                entityQuery = entityQuery.OrderBy(pageOrderModel);
            var query = Query(entityQuery);
            var list = await query.Paginate(pageOrderModel).ToListAsync(cancellationToken);
            return pageOrderModel.TotalRecordsCount > 0 ? new SuccessResponse<List<TQueryModel>>(pageOrderModel.TotalRecordsCount + " kayıt bulundu.", list) : new ErrorResponse<List<TQueryModel>>("Kayıt bulunamadı!");
        }

        public virtual async Task<Response<TQueryModel>> GetItem(int id, CancellationToken cancellationToken = default)
        {
            var item = await Query().SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
            return item is not null ? new SuccessResponse<TQueryModel>(item) : new ErrorResponse<TQueryModel>("Kayıt bulunamadı!");
        }

        public virtual async Task<Response<TCommandModel>> GetItemCommand(int id, CancellationToken cancellationToken = default)
        {
            var item = await QueryCommand().SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
            return item is not null ? new SuccessResponse<TCommandModel>(item) : new ErrorResponse<TCommandModel>("Kayıt bulunamadı!");
        }

        public virtual async Task<Response<TCommandModel>> Create(TCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map(commandModel);
            _repo.Create(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse<TCommandModel>("Kayıt başarıyla oluşturuldu.", _mapper.Map(entity));
        }

        public virtual async Task<Response<TCommandModel>> Update(TCommandModel commandModel, CancellationToken cancellationToken = default)
        {
            var entity = await _repo.Query().SingleOrDefaultAsync(q => q.Id == commandModel.Id);
            _repo.Update(_mapper.Map(commandModel, entity));
            try
            {
                await _unitOfWork.SaveAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ErrorResponse<TCommandModel>("Kayıt bulunamadı!");
            }
            return new SuccessResponse<TCommandModel>("Kayıt başarıyla güncellendi.", _mapper.Map(entity));
        }

        public virtual async Task<Response<TCommandModel>> Delete(int id, CancellationToken cancellationToken = default)
        {
            _repo.Delete(e => e.Id == id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse<TCommandModel>("Kayıt başarıyla silindi.");
        }

        public void Dispose()
        {
            _repo.Dispose();
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
