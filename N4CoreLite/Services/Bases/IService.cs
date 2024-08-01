using AutoMapper;
using N4CoreLite.Models;
using N4CoreLite.Responses.Bases;

namespace N4CoreLite.Services.Bases
{
    public interface IService<TEntity, TQueryModel, TCommandModel> : IDisposable
    {
        public void Set(params Profile[] mapperProfiles);
        public IQueryable<TQueryModel> Query(bool noEntityTracking = true);
        public IQueryable<TQueryModel> Query(IQueryable<TEntity> entityQuery);
        public IQueryable<TCommandModel> QueryCommand();
        public Task<Response<List<TQueryModel>>> GetList(CancellationToken cancellationToken = default);
        public Task<Response<List<TQueryModel>>> GetList(PageOrderModel pageOrderModel, CancellationToken cancellationToken = default);
        public Task<Response<TQueryModel>> GetItem(int id, CancellationToken cancellationToken = default);
        public Task<Response<TCommandModel>> GetItemCommand(int id, CancellationToken cancellationToken = default);
        public Task<Response<TCommandModel>> Create(TCommandModel commandModel, CancellationToken cancellationToken = default);
        public Task<Response<TCommandModel>> Update(TCommandModel commandModel, CancellationToken cancellationToken = default);
        public Task<Response<TCommandModel>> Delete(int id, CancellationToken cancellationToken = default);
    }
}
