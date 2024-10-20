using Implementation.Interfaces;
using Implementation.Utility.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Implementation
{
    public class ImageManager : IImageManager
    {
        private readonly IUsefulFunctions _usefulFunctions;
        private const int ColorByteSize = 3; // Taille d'une couleur en bytes (R, G, B)

        public ImageManager(IUsefulFunctions usefulFunctions)
        {
            _usefulFunctions = usefulFunctions;
        }

        public void FillImageWithBytes(Image image, int squareSize, byte[] byteArray)
        {
            ValidateImageParameters(image, squareSize, byteArray);

            int indexArray = 0;
            image.Mutate(ctx =>
            {
                for (int x = 0; x < image.Width; x += squareSize)
                {
                    for (int y = 0; y < image.Height; y += squareSize)
                    {
                        if (indexArray >= byteArray.Length)
                            return; // S'assurer de ne pas dépasser les limites du tableau

                        // Obtenir la couleur à partir du tableau de bytes
                        Color squareColor = _usefulFunctions.GetColorFromArray(byteArray, indexArray);
                        indexArray += ColorByteSize;

                        // Dessiner le carré avec la couleur déterminée
                        ctx.Fill(squareColor, new RectangleF(x, y, squareSize, squareSize));
                    }
                }
            });
        }

        public byte[] ReadImageBySquares(Image<Rgba32> image, int squareSize, byte[] byteArray)
        {
            ValidateImageParameters(image, squareSize, byteArray);
            
            byte[] byteArrayResult = new byte[byteArray.Length];
            int indexArray = 0;

            for (int x = 0; x < image.Width; x += squareSize)
            {
                for (int y = 0; y < image.Height; y += squareSize)
                {
                    if (indexArray >= byteArray.Length)
                        return byteArrayResult; // Eviter de dépasser les limites du tableau

                    // Lire la couleur du pixel central du carré
                    Rgba32 pixelColor = image[x, y];

                    // Convertir la couleur en bytes et remplir le tableau
                    byteArrayResult = _usefulFunctions.GetByteFromColor(byteArrayResult, indexArray, pixelColor);
                    indexArray += ColorByteSize;
                }
            }
            return byteArrayResult;
        }

        private void ValidateImageParameters(Image image, int squareSize, byte[] byteArray)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), Constantes.ERROR_IMAGE_REQUIRED);
            if (squareSize <= 0)
                throw new ArgumentException(Constantes.ERROR_INVALID_SQUARE_SIZE);
            if (byteArray == null || byteArray.Length <= 0)
                throw new ArgumentException(Constantes.ERROR_DATA_REQUIRED);
            if (image.Width % squareSize != 0 || image.Height % squareSize != 0)
                throw new ArgumentException(Constantes.ERROR_SIZE_SQUARE);
        }
    }

}