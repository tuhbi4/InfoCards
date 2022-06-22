using InfoCards.DAL.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace InfoCards.DAL.DAO
{
    public class JsonDeserializer : IDeserializer
    {
        public List<T> GetData<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            List<T> itemList = JsonConvert.DeserializeObject<List<T>>(jsonString);

            return itemList;
        }
    }
}