namespace DataLayer.Repository.Interface;

public interface IRepository<TModel,TId> where TModel:class
{
    Task<IList<TModel?>> GetAll(CancellationToken cancellationToken = default);
    Task<TModel?> GetById(TId? id, CancellationToken cancellationToken=default);
    Task<TModel?> Insert(TModel? entity, CancellationToken cancellationToken=default);
    Task Delete(TModel entity, CancellationToken cancellationToken=default);
    Task Save(CancellationToken cancellationToken=default);
}