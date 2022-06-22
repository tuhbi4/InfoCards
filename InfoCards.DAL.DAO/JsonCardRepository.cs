using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InfoCards.DAL.DAO
{
    public class JsonCardRepository : IRepository<Card>
    {
        private List<Card> Items { get; set; }

        private readonly string jsonFilePath;
        private readonly ISerializer jsonSerializer;
        private readonly IDeserializer jsonDeserializer;

        public JsonCardRepository(ISerializer jsonSerializer, IDeserializer jsonDeserializer, string jsonFilePath)
        {
            Items = new List<Card>();
            this.jsonSerializer = jsonSerializer;
            this.jsonDeserializer = jsonDeserializer;
            this.jsonFilePath = jsonFilePath;
        }

        public void ReadAll()
        {
            Items = jsonDeserializer.GetData<Card>(jsonFilePath);
        }

        public List<Card> GetAll()
        {
            return Items;
        }

        public void Create(Card dataObject)
        {
            Items.Add(dataObject);
        }

        public Card Read(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Card dataObject)
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