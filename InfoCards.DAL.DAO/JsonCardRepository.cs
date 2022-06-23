using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InfoCards.DAL.DAO
{
    public class JsonCardRepository : IRepository<InfoCard>
    {
        private readonly string jsonFilePath;

        public JsonCardRepository(string jsonFilePath)
        {
            this.jsonFilePath = jsonFilePath;
        }

        public List<InfoCard> GetAll()
        {
            List<InfoCard> itemList = ReadFromFile();

            return itemList;
        }

        public void Create(InfoCard dataObject)
        {
            List<InfoCard> itemList = ReadFromFile();

            itemList.Add(dataObject);

            SaveToFile(itemList);
        }

        public InfoCard Read(int id)
        {
            List<InfoCard> itemList = ReadFromFile();

            return itemList.FirstOrDefault(x => x.Id == id);
        }

        public void Update(InfoCard dataObject)
        {
            List<InfoCard> itemList = ReadFromFile();

            itemList.Remove(itemList.FirstOrDefault(x => x.Id == dataObject.Id));
            itemList.Add(dataObject);

            SaveToFile(itemList);
        }

        public void Delete(int id)
        {
            List<InfoCard> itemList = ReadFromFile();

            itemList.Remove(itemList.FirstOrDefault(x => x.Id == id));

            SaveToFile(itemList);
        }

        private List<InfoCard> ReadFromFile()
        {
            string fiileString = File.ReadAllText(jsonFilePath);
            List<InfoCard> itemList = JsonConvert.DeserializeObject<List<InfoCard>>(fiileString);

            return itemList;
        }

        private void SaveToFile(List<InfoCard> itemList)
        {
            string jsonString = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}