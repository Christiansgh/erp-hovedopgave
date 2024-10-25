namespace erp_hovedopgave.Repositories;
public interface IRepository<T, K> {
  List<T> ReadAll();
  T ReadById(K id);
  T Create(T model);
  T Update(T model);
  T Delete(T model);
}
