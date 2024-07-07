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
    }
}
