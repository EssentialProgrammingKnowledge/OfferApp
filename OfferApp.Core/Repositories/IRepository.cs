using OfferApp.Core.Entities;

namespace OfferApp.Core.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<int> Add(T entity);
        Task<bool> Update(T entity);
        Task Delete(T entity);
        Task<T?> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
    }
}
