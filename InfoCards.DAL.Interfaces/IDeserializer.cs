using System.Collections.Generic;

namespace InfoCards.DAL.Interfaces
{
    public interface IDeserializer
    {
        public List<T> GetData<T>(string filePath);
    }
}