namespace Implementation.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);
        byte[] ReadAllBytes(string path);
    }
}
