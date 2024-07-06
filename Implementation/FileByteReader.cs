
using Implementation.Interfaces;

namespace Implementation
{
    public class FileByteReader: IFileByteReader
    {
        private readonly IFileWrapper _file;
        private readonly IFileLogger _fileLogger;
        public FileByteReader(IFileWrapper fileWrapper, IFileLogger fileLogger) 
        {
            _file = fileWrapper;
            _fileLogger = fileLogger;
        }
        public byte[] ReadFileBytes(string filePath)
        {
            byte[] fileBytes;

            try
            {
                // Log the start of the read operation
                _fileLogger.LogInfo($"Starting to read file: {filePath}");

                // Vérifier si le fichier existe
                if (!_file.Exists(filePath))
                {
                    _fileLogger.LogWarning($"File not found: {filePath}");
                    throw new FileNotFoundException("Le fichier spécifié n'existe pas.", filePath);
                }

                // Lire tous les bytes du fichier
                fileBytes = _file.ReadAllBytes(filePath);

                // Log successful read
                _fileLogger.LogInfo($"Successfully read file: {filePath}");
            }
            catch (Exception ex)
            {
                _fileLogger.LogError($"Error reading file: {filePath}", ex);
                throw; // Rejeter l'exception pour une gestion ultérieure si nécessaire
            }

            return fileBytes;
        }
    }
}
