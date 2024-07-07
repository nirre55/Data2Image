
namespace Implementation.Tests
{
    public class FileLoggerTests
    {
        [Fact]
        public void LogInfo_WritesInfoLogToFile()
        {
            // Arrange
            var logger = new FileLogger();
            var message = "This is an info message.";

            // Act
            logger.LogInfo(message);

            // Assert
            var logContent = File.ReadAllText(Constantes.LOG_FILE_PATH);
            Assert.Contains("INFO", logContent);
            Assert.Contains(message, logContent);

            // Cleanup
            File.Delete(Constantes.LOG_FILE_PATH);
        }

        [Fact]
        public void LogWarning_WritesWarningLogToFile()
        {
            // Arrange
            var logger = new FileLogger();
            var message = "This is a warning message.";

            // Act
            logger.LogWarning(message);

            // Assert
            var logContent = File.ReadAllText(Constantes.LOG_FILE_PATH);
            Assert.Contains("WARNING", logContent);
            Assert.Contains(message, logContent);

            // Cleanup
            File.Delete(Constantes.LOG_FILE_PATH);
        }

        [Fact]
        public void LogError_WritesErrorLogToFile()
        {
            // Arrange
            var logger = new FileLogger();
            var message = "This is an error message.";
            var exception = new Exception("Test exception");

            // Act
            logger.LogError(message, exception);

            // Assert
            var logContent = File.ReadAllText(Constantes.LOG_FILE_PATH);
            Assert.Contains("ERROR", logContent);
            Assert.Contains(message, logContent);
            Assert.Contains(exception.Message, logContent);

            // Cleanup
            File.Delete(Constantes.LOG_FILE_PATH);
        }
    }
}
