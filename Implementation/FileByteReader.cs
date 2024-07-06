
using Implementation.Interfaces;

namespace Implementation
{
    public class FileByteReader : IFileByteReader
    {
        private readonly IFileWrapper _file;
        public FileByteReader(IFileWrapper fileWrapper) 
        {
            _file = fileWrapper;
        }
        public byte[] ReadFileBytes(string filePath)
        {
            byte[] fileBytes;

            try
            {
                // Vérifier si le fichier existe
                if (!_file.Exists(filePath))
                {
                    throw new FileNotFoundException("Le fichier spécifié n'existe pas.", filePath);
                }

                // Lire tous les bytes du fichier
                fileBytes = _file.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
                throw; // Rejeter l'exception pour une gestion ultérieure si nécessaire
            }

            return fileBytes;
        }
    }
}
