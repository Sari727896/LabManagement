
namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
    }
}
