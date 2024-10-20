using Implementation.Wrapper.Interfaces;
using Newtonsoft.Json;

namespace Implementation.Wrapper
{
    public class JsonConvertWrapper: IJsonConvertWrapper
    {
        public string SerializeObject<T>(T objectToSerialize, Formatting formatting)
        {
            try
            {
                return JsonConvert.SerializeObject(objectToSerialize, formatting);
            }
            catch (JsonSerializationException)
            {
                throw new InvalidOperationException("Failed to serialize the object to JSON.");
            }          
        }

        public T DeserializeObject<T>(string fileContents)
        {        
            try
            {
                var result = JsonConvert.DeserializeObject<T>(fileContents);
                if (result == null)
                {
                    throw new InvalidOperationException("Deserialization returned null.");
                }
                return result;
            }
            catch (JsonReaderException)
            {
                throw new InvalidOperationException("Invalid JSON data.");
            }
        }
    }
    
}
    
