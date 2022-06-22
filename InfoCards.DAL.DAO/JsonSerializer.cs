using InfoCards.DAL.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace InfoCards.DAL.DAO
{
    public class JsonSerializer : ISerializer
    {
        public void SaveData<T>(string filePath, List<T> dataObject)
        {
            string jsonString = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
    }
}