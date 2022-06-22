using System.Collections.Generic;

namespace InfoCards.DAL.Interfaces
{
    public interface ISerializer
    {
        public void SaveData<T>(string filePath, List<T> dataObject);
    }
}
