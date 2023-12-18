namespace IyzicoApp.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        void Create(T entity);
        T Read(Guid id);
        void Update(T entity);
        void Delete(T entity);
    }
}
