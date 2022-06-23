using InfoCards.Common.Entities;
using Moq;
using NUnit.Framework;

namespace InfoCards.DAL.DAO.Tests
{
    [TestFixture]
    public class JsonCardRepositoryTests
    {
        private const string JsonPath = "..\\..\\..\\..\\data\\cards.json";

        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Test]
        public void GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = new JsonCardRepository(JsonPath);

            // Act
            var result = jsonCardRepository.GetAll();

            // Assert
            Assert.True(result.Count > 0);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int id = 199;
            var jsonCardRepository = new JsonCardRepository(JsonPath);
            InfoCard dataObject = new InfoCard { Id = id, Name = "TestName", ImageBase64 = "/9j/4QAYRXhpZgAASUkqAAgyvptUWk661UQ70A0oj/2Q==", };

            // Act
            var startItemCount = jsonCardRepository.GetAll().Count;
            jsonCardRepository.Create(dataObject);
            var endItemCount = jsonCardRepository.GetAll().Count;

            // Assert
            Assert.AreEqual(startItemCount + 1, endItemCount);
            mockRepository.VerifyAll();
            jsonCardRepository.Delete(id);
        }

        [Test]
        public void Read_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int id = 299;
            var jsonCardRepository = new JsonCardRepository(JsonPath);
            InfoCard dataObject = new InfoCard { Id = id, Name = "TestName", ImageBase64 = "/9j/4QAYRXhpZgAASUkqAAgyvptUWk661UQ70A0oj/2Q==", };

            // Act
            jsonCardRepository.Create(dataObject);
            var result = jsonCardRepository.Read(id);

            // Assert
            Assert.AreEqual(dataObject.Id, result.Id);
            Assert.AreEqual(dataObject.Name, result.Name);
            Assert.AreEqual(dataObject.ImageBase64, result.ImageBase64);
            mockRepository.VerifyAll();
            jsonCardRepository.Delete(id);
        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int id = 399;
            string Name = "NewTestName";
            var jsonCardRepository = new JsonCardRepository(JsonPath);
            InfoCard dataObject = new InfoCard { Id = id, Name = "TestName", ImageBase64 = "/9j/4QAYRXhpZgAASUkqAAgyvptUWk661UQ70A0oj/2Q==", };
            InfoCard dataObjectUpdated = new InfoCard { Id = id, Name = Name, ImageBase64 = "/9j/4QAYRXhpZgAASUkqAAgyvptUWk661UQ70A0oj/2Q==", };

            // Act
            jsonCardRepository.Create(dataObject);
            jsonCardRepository.Update(dataObjectUpdated);
            var result = jsonCardRepository.Read(
                id);

            // Assert
            Assert.AreEqual(dataObject.Id, result.Id);
            Assert.AreEqual(Name, result.Name);
            Assert.AreEqual(dataObject.ImageBase64, result.ImageBase64);
            mockRepository.VerifyAll();
            jsonCardRepository.Delete(id);
        }

        [Test]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int id = 399;
            var jsonCardRepository = new JsonCardRepository(JsonPath);
            InfoCard dataObject = new InfoCard { Id = id, Name = "TestName", ImageBase64 = "/9j/4QAYRXhpZgAASUkqAAgyvptUWk661UQ70A0oj/2Q==", };

            // Act
            var startItemCount = jsonCardRepository.GetAll().Count;
            jsonCardRepository.Create(dataObject);
            jsonCardRepository.Delete(id);
            var endItemCount = jsonCardRepository.GetAll().Count;

            // Assert
            Assert.AreEqual(startItemCount, endItemCount);
            mockRepository.VerifyAll();
        }
    }
}