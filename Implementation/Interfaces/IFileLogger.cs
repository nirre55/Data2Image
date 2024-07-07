
namespace Implementation.Interfaces
{
    public interface IFileLogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
}
