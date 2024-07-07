
using AutoFixture;
using FluentAssertions;
using Implementation.Interfaces;
using Moq;

namespace Implementation.Tests
{
    public class FileByteReaderTests
    {
        private readonly Mock<IFileLogger> _fileLoggerMock;
        private readonly Mock<IFileWrapper> _fileWrapperMock;
        private readonly FileByteReader _fileReader;
        private readonly Fixture _fixure;
        public FileByteReaderTests()
        {
            _fileLoggerMock = new Mock<IFileLogger>();
            _fileWrapperMock = new Mock<IFileWrapper>();
            _fixure = new Fixture();
            _fileReader = new FileByteReader(_fileWrapperMock.Object, _fileLoggerMock.Object);
        }

        [Fact]
        public void ReadFileBytesSuccess_FileExists_LogAllInfo()
        {
            // Arrange
            var filePath = "testfile.txt";
            var arrayOfBytes = new byte[] { 1, 2, 3, 4 };

            _fileWrapperMock.Setup(fw => fw.Exists(filePath)).Returns(true);
            _fileWrapperMock.Setup(fw => fw.ReadAllBytes(filePath)).Returns(arrayOfBytes);

            // Act
            _fileReader.ReadFileBytes(filePath);

            // Assert
            _fileLoggerMock.Verify(fl => fl.LogInfo($"Starting to read file: {filePath}"), Times.Once);
            _fileLoggerMock.Verify(fl => fl.LogInfo($"Successfully read file: {filePath}"), Times.Once);
            _fileLoggerMock.Verify(fl => fl.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
        }

        [Fact]
        public void ReadFileBytes_FileExists_ReturnsFileBytes()
        {
            // Arrange
            var filePath = "testfile.txt";
            var expectedBytes = new byte[] { 1, 2, 3, 4 };

            _fileWrapperMock.Setup(fw => fw.Exists(filePath)).Returns(true);
            _fileWrapperMock.Setup(fw => fw.ReadAllBytes(filePath)).Returns(expectedBytes);

            // Act
            var result = _fileReader.ReadFileBytes(filePath);

            // Assert
            _fileLoggerMock.Verify(fl => fl.LogInfo($"Starting to read file: {filePath}"), Times.Once);
            _fileLoggerMock.Verify(fl => fl.LogInfo($"Successfully read file: {filePath}"), Times.Once);

            // Assert
            result.Should().BeEquivalentTo(expectedBytes);
        }

        [Fact]
        public void ReadFileBytes_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange
            var filePath = "nonexistentfile.txt";

            _fileWrapperMock.Setup(fw => fw.Exists(filePath)).Returns(false);

            // Act
            var exception = Assert.Throws<FileNotFoundException>(() => _fileReader.ReadFileBytes(filePath));

            // Assert
            Assert.Equal("Le fichier spécifié n'existe pas.", exception.Message);
        }

        [Fact]
        public void ReadFileBytes_ExceptionThrown_LogsError()
        {
            // Arrange
            var filePath = "testfile.txt";

            _fileWrapperMock.Setup(fw => fw.Exists(filePath)).Returns(true);
            _fileWrapperMock.Setup(fw => fw.ReadAllBytes(filePath)).Throws(new IOException("Test exception"));

            var exception = Assert.Throws<IOException>(() => _fileReader.ReadFileBytes(filePath));

            // Act & Assert
            Assert.Equal("Test exception", exception.Message);
            _fileLoggerMock.Verify(fl => fl.LogError($"Error reading file: {filePath}", It.IsAny<IOException>()), Times.Once);
        }
    }
}
