using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InfoCards.DAL.DAO
{
    public class JsonCardRepository : IRepository<InfoCard>
    {
        private List<InfoCard> Items { get; set; }

        private readonly string jsonFilePath;
        private readonly ISerializer jsonSerializer;
        private readonly IDeserializer jsonDeserializer;

        public JsonCardRepository(ISerializer jsonSerializer, IDeserializer jsonDeserializer, string jsonFilePath)
        {
            Items = new List<InfoCard>();
            this.jsonSerializer = jsonSerializer;
            this.jsonDeserializer = jsonDeserializer;
            this.jsonFilePath = jsonFilePath;
        }

        public void ReadAll()
        {
            Items = jsonDeserializer.GetData<InfoCard>(jsonFilePath);
        }

        public List<InfoCard> GetAll()
        {
            return Items;
        }

        public void Create(InfoCard dataObject)
        {
            Items.Add(dataObject);
        }

        public InfoCard Read(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public void Update(InfoCard dataObject)
        {
            Items.Insert(Items.IndexOf(Read(dataObject.Id)), dataObject);
        }

        public void Delete(int id)
        {
            Items.Remove(Read(id));
        }

        public void Save()
        {
            jsonSerializer.SaveData(jsonFilePath, Items);
        }
    }
}