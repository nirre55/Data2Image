using AutoFixture;
using FluentAssertions;
using Implementation.Entities;
using Implementation.Interfaces;
using Implementation.Wrapper.Interfaces;
using Moq;
using Newtonsoft.Json;

namespace Implementation.Tests
{
    public class JsonFileManagerTests
    {
        private readonly IJsonFileManager _jsonFileManager;
        private readonly Mock<IFileWrapper> _fileWrapperMock;
        private readonly Mock<IJsonConvertWrapper> _jsonConvertWrapperMock;
        private readonly Fixture _fixure;

        public JsonFileManagerTests()
        {
            _fileWrapperMock = new Mock<IFileWrapper>();
            _jsonConvertWrapperMock = new Mock<IJsonConvertWrapper>();
            _jsonFileManager = new JsonFileManager(_fileWrapperMock.Object, _jsonConvertWrapperMock.Object);
            _fixure = new Fixture();
        }

        [Fact]
        public void ReadFromJsonFile_ShouldReturnDeserializedObject_WhenFileContainsValidJson()
        {
            // Arrange
            string filePath = "test.json";
            string fileContents = "{\"Name\":\"Test\",\"Age\":30}";
            var expectedObject = new TestObject { Name = "Test", Age = 30 };

            _fileWrapperMock.Setup(f => f.ReadAllText(filePath)).Returns(fileContents);
            _jsonConvertWrapperMock.Setup(j => j.DeserializeObject<TestObject>(fileContents)).Returns(expectedObject);

            // Act
            var result = _jsonFileManager.ReadFromJsonFile<TestObject>(filePath);

            // Assert
            result.Should().BeEquivalentTo(expectedObject);
        }

        [Fact]
        public void ReadFromJsonFile_ShouldCallFileWrapperReadAllText()
        {
            // Arrange
            string filePath = "test.json";
            string fileContents = "{\"Name\":\"Test\",\"Age\":30}";
            _fileWrapperMock.Setup(f => f.ReadAllText(filePath)).Returns(fileContents);

            // Act
            _jsonFileManager.ReadFromJsonFile<dynamic>(filePath);

            // Assert
            _fileWrapperMock.Verify(f => f.ReadAllText(filePath), Times.Once);
        }

        [Fact]
        public void ReadFromJsonFile_ShouldCallJsonDeserializer()
        {
            // Arrange
            string filePath = "test.json";
            string fileContents = "{\"Name\":\"Test\",\"Age\":30}";
            _fileWrapperMock.Setup(f => f.ReadAllText(filePath)).Returns(fileContents);

            // Act
            _jsonFileManager.ReadFromJsonFile<dynamic>(filePath);

            // Assert
            _jsonConvertWrapperMock.Verify(j => j.DeserializeObject<dynamic>(fileContents), Times.Once);
        }

        [Fact]
        public void WriteToJsonFile_ShouldSerializeObject()
        {
            // Arrange
            string filePath = "test.json";
            var objectToWrite = new { Name = "Test", Age = 30 };
            string serializedContents = "{\n  \"Name\": \"Test\",\n  \"Age\": 30\n}";

            _jsonConvertWrapperMock.Setup(j => j.SerializeObject(objectToWrite, Newtonsoft.Json.Formatting.Indented))
                .Returns(serializedContents);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite, append: false);

            // Assert
            _jsonConvertWrapperMock.Verify(j => j.SerializeObject(objectToWrite, Formatting.Indented), Times.Once);
        }

        [Fact]
        public void WriteToJsonFile_ShouldWriteSerializedObjectToFile_WhenAppendIsFalse()
        {
            // Arrange
            string filePath = "test.json";
            var objectToWrite = new { Name = "Test", Age = 30 };
            string serializedContents = "{\n  \"Name\": \"Test\",\n  \"Age\": 30\n}";

            _jsonConvertWrapperMock.Setup(j => j.SerializeObject(objectToWrite, Newtonsoft.Json.Formatting.Indented))
                .Returns(serializedContents);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite, append: false);

            // Assert
            _fileWrapperMock.Verify(f => f.WriteAllText(filePath, serializedContents), Times.Once);
        }

        [Fact]
        public void WriteToJsonFile_ShouldNotAppendToFile_WhenAppendIsFalse()
        {
            // Arrange
            string filePath = "test.json";
            var objectToWrite = new { Name = "Test", Age = 30 };
            string serializedContents = "{\n  \"Name\": \"Test\",\n  \"Age\": 30\n}";

            _jsonConvertWrapperMock.Setup(j => j.SerializeObject(objectToWrite, Newtonsoft.Json.Formatting.Indented))
                .Returns(serializedContents);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite, append: false);

            // Assert
            _fileWrapperMock.Verify(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WriteToJsonFile_ShouldAppendSerializedObjectToFile_WhenAppendIsTrue()
        {
            // Arrange
            string filePath = "test.json";
            var objectToWrite = new { Name = "Test", Age = 30 };
            string serializedContents = "{\n  \"Name\": \"Test\",\n  \"Age\": 30\n}";

            _jsonConvertWrapperMock.Setup(j => j.SerializeObject(objectToWrite, Formatting.Indented))
                .Returns(serializedContents);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite, append: true);

            // Assert
            _fileWrapperMock.Verify(f => f.AppendAllText(filePath, serializedContents), Times.Once);
        }

        [Fact]
        public void WriteToJsonFile_ShouldNotOverwriteFile_WhenAppendIsTrue()
        {
            // Arrange
            string filePath = "test.json";
            var objectToWrite = new { Name = "Test", Age = 30 };
            string serializedContents = "{\n  \"Name\": \"Test\",\n  \"Age\": 30\n}";

            _jsonConvertWrapperMock.Setup(j => j.SerializeObject(objectToWrite, Formatting.Indented))
                .Returns(serializedContents);

            // Act
            _jsonFileManager.WriteToJsonFile(filePath, objectToWrite, append: true);

            // Assert
            _fileWrapperMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        public class TestObject
        {
            public string? Name { get; set; }
            public int Age { get; set; }
        }
    }
}
