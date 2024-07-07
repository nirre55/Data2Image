
namespace Implementation.Tests
{
    public class FileWrapperTests
    {
        [Fact]
        public void Exists_FileExists_ReturnsTrue()
        {
            // Arrange
            var filePath = "testfile.txt";
            File.WriteAllText(filePath, "Test content");

            var fileWrapper = new FileWrapper();

            // Act
            var result = fileWrapper.Exists(filePath);

            // Assert
            Assert.True(result);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void Exists_FileDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var filePath = "nonexistentfile.txt";

            var fileWrapper = new FileWrapper();

            // Act
            var result = fileWrapper.Exists(filePath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ReadAllBytes_FileExists_ReturnsFileBytes()
        {
            // Arrange
            var filePath = "testfile.txt";
            var expectedBytes = new byte[] { 1, 2, 3, 4 };
            File.WriteAllBytes(filePath, expectedBytes);

            var fileWrapper = new FileWrapper();

            // Act
            var result = fileWrapper.ReadAllBytes(filePath);

            // Assert
            Assert.Equal(expectedBytes, result);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadAllBytes_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange
            var filePath = "nonexistentfile.txt";

            var fileWrapper = new FileWrapper();

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => fileWrapper.ReadAllBytes(filePath));
        }
    }
}
