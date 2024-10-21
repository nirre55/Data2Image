using Implementation.Interfaces;
using Implementation.Wrapper;
using Implementation.Wrapper.Interfaces;
using Newtonsoft.Json;

namespace Implementation
{
    public class JsonFileManager : IJsonFileManager
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IJsonConvertWrapper _jsonSerializerWrapper;
        public JsonFileManager(IFileWrapper fileWrapper, IJsonConvertWrapper jsonSerializerWrapper) 
        { 
            _fileWrapper = fileWrapper;
            _jsonSerializerWrapper = jsonSerializerWrapper;
        }

        public T ReadFromJsonFile<T>(string filePath)
        {
            var fileContents = _fileWrapper.ReadAllText(filePath);
            return _jsonSerializerWrapper.DeserializeObject<T>(fileContents);  
        }

        public void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            var contentsToWriteToFile = _jsonSerializerWrapper.SerializeObject(objectToWrite, Formatting.Indented);

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
