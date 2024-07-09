using Implementation.Interfaces;

namespace Implementation
{
    public class FileWrapper: IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void WriteAllText(string filePath, string contents)
        {
            File.WriteAllText(filePath, contents);
        }

        public void AppendAllText(string filePath, string contents)
        {
            File.AppendAllText(filePath, contents);
        }
    }
}
