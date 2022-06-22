using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace InfoCards.DAL.DAO.Tests
{
    [TestFixture]
    public class JsonCardRepositoryTests
    {
        private const string JsonPath = "..\\..\\..\\..\\data\\cards.json";

        private MockRepository mockRepository;

        private Mock<ISerializer> mockSerializer;
        private Mock<IDeserializer> mockDeserializer;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);

            mockSerializer = mockRepository.Create<ISerializer>();
            mockDeserializer = mockRepository.Create<IDeserializer>();
        }

        [Test]
        public void GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();

            // Act
            jsonCardRepository.ReadAll();
            var result = jsonCardRepository.GetAll();

            // Assert
            Assert.True(result.Count > 0);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();
            Card dataObject = new Card { Id = 999, ImgFilename = "TestImgFileName.jpg", Name = "TestName" };

            // Act
            jsonCardRepository.ReadAll();
            var startItemCount = jsonCardRepository.GetAll().Count;
            jsonCardRepository.Create(dataObject);
            var endItemCount = jsonCardRepository.GetAll().Count;

            // Assert
            Assert.True(startItemCount + 1 == endItemCount);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Read_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();
            Card dataObject = new Card { Id = 999, ImgFilename = "TestImgFileName.jpg", Name = "TestName" };
            int id = 999;

            // Act
            jsonCardRepository.ReadAll();
            jsonCardRepository.Create(dataObject);
            var result = jsonCardRepository.Read(
                id);

            // Assert
            Assert.AreEqual(dataObject, result);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();
            Card dataObject = new Card { Id = 999, ImgFilename = "TestImgFileName.jpg", Name = "TestName" };
            Card dataObjectUpdated = new Card { Id = 999, ImgFilename = "newTestImgFileName.jpg", Name = "NewTestName" };
            int id = 999;

            // Act
            jsonCardRepository.ReadAll();
            jsonCardRepository.Create(dataObject);
            jsonCardRepository.Update(dataObjectUpdated);
            var result = jsonCardRepository.Read(
                id);

            // Assert
            Assert.AreEqual(dataObjectUpdated, result);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();
            Card dataObject = new Card { Id = 999, ImgFilename = "TestImgFileName.jpg", Name = "TestName" };

            // Act
            jsonCardRepository.ReadAll();
            var startItemCount = jsonCardRepository.GetAll().Count;
            jsonCardRepository.Create(dataObject);
            jsonCardRepository.Delete(999);
            var endItemCount = jsonCardRepository.GetAll().Count;

            // Assert
            Assert.True(startItemCount == endItemCount);
            mockRepository.VerifyAll();
        }

        [Test]
        public void Save_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var jsonCardRepository = CreateJsonCardRepository();
            Card dataObject = new Card { Id = 999, ImgFilename = "TestImgFileName.jpg", Name = "TestName" };

            // Act
            jsonCardRepository.ReadAll();
            var startItemList = jsonCardRepository.GetAll().Count;
            jsonCardRepository.Create(dataObject);
            jsonCardRepository.Save();
            jsonCardRepository.ReadAll();
            var endItemList = jsonCardRepository.GetAll().Count;

            // Assert
            Assert.True(startItemList + 1 == endItemList);
            mockRepository.VerifyAll();
            jsonCardRepository.Delete(999);
            jsonCardRepository.Save();
        }

        private JsonCardRepository CreateJsonCardRepository()
        {
            return new JsonCardRepository(new JsonSerializer(), new JsonDeserializer(), JsonPath);
        }
    }
}