using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Interfaces
{
    public interface IJsonFileManager
    {
        void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false);
        T ReadFromJsonFile<T>(string filePath);
    }
}
