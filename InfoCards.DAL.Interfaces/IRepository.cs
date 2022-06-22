using System.Collections.Generic;

namespace InfoCards.DAL.Interfaces
{
    public interface IRepository<T>
    {
        public void ReadAll();

        public List<T> GetAll();

        public void Create(T dataObject);

        public T Read(int id);

        public void Update(T dataObject);

        public void Delete(int id);

        public void Save();
    }
}