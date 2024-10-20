using Newtonsoft.Json;

namespace Implementation.Wrapper.Interfaces
{
    public interface IJsonConvertWrapper
    {
        string SerializeObject<T>(T objectToSerialize, Formatting formatting);
        T DeserializeObject<T>(string fileContents);
    }

}
