namespace CrazyUniverse.Core
{
    public interface IRepository<T> where T : Robot
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}