namespace Implementation.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);
        byte[] ReadAllBytes(string path);
        string ReadAllText(string filePath);
        void WriteAllText(string filePath, string contents);
        void AppendAllText(string filePath, string contents);
    }
}
