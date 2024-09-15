using Implementation.Interfaces;
using Newtonsoft.Json;

namespace Implementation
{
    public class JsonFileManager : IJsonFileManager
    {
        private readonly IFileWrapper _fileWrapper;
        public JsonFileManager(IFileWrapper fileWrapper) 
        { 
            _fileWrapper = fileWrapper;                       
        }
        public T ReadFromJsonFile<T>(string filePath)
        {
            var fileContents = _fileWrapper.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(fileContents);
        }

        public void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
            if (append)
            {
                _fileWrapper.AppendAllText(filePath, contentsToWriteToFile);
            }
            else
            {
                _fileWrapper.WriteAllText(filePath, contentsToWriteToFile);
            }
        }
    }
}
