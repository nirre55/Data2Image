using AutoFixture;
using FluentAssertions;
using Implementation.Entities;
using Implementation.Interfaces;
using Moq;
using Newtonsoft.Json;

namespace Implementation.Tests
{
    public class JsonFileManagerTests
    {
        private readonly IJsonFileManager _jsonFileManager;
        private readonly Mock<IFileWrapper> _fileWrapperMock;
        private readonly Fixture _fixure;

        public JsonFileManagerTests()
        {
            _fileWrapperMock = new Mock<IFileWrapper>();
            _jsonFileManager = new JsonFileManager(_fileWrapperMock.Object);
            _fixure = new Fixture();
        }

        [Fact]
        public void ReadFromJsonFile_ShouldReturnCorrectObject()
        {
            // Arrange
            var filePath = "test.json";
            var expectedObject = _fixure.Create<EJsonFile>();
            var fileContents = JsonConvert.SerializeObject(expectedObject);

            _fileWrapperMock.Setup(fm => fm.ReadAllText(filePath)).Returns(fileContents);

            // Act
            var result = _jsonFileManager.ReadFromJsonFile<EJsonFile>(filePath);

            // Assert
            result.Should().BeEquivalentTo(expectedObject);
        }

        [Fact]
        public void WriteToJsonFile_ShouldWriteCorrectContent()
        {
            // Arrange
            var filePath = "test.json";
            var objectToWrite = _fixure.Create<EJsonFile>();
            var expectedContent = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite);

            // Assert
            _fileWrapperMock.Verify(fm => fm.WriteAllText(filePath, expectedContent), Times.Once);
        }
    }
}
