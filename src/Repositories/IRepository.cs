namespace erp.Repositories;

public interface IRepository<T, K, R> {
  Task<List<T>> ReadAllAsync();
  Task<T> ReadByIdAsync(K id);
  Task<R> CreateAsync(T model);
  Task<R> UpdateAsync(T model);
  Task<R> DeleteAsync(T model);
}
