using Implementation.Interfaces;
using Implementation.Utility.Interfaces;
using Implementation.Wrapper.Interfaces;
using System.Drawing;

namespace Implementation
{
    public class ImageManager : IImageManager
    {
        private readonly IGraphics _graphics;
        private readonly IUsefulFunctions _usefulFunctions;
        public ImageManager(IGraphics graphics, IUsefulFunctions usefulFunctions)
        {
            _graphics = graphics;
            _usefulFunctions = usefulFunctions;
        }

        public void FillImageWithBytes(Bitmap image, int squareSize, byte[] byte_array)
        {
            int indexArray = 0;
            if (byte_array.Length <= 0  || squareSize <= 0)
            {
                throw new ArgumentException(Constantes.ERROR_DATA_REQUIRED);
            }

            if (image.Width % squareSize != 0 || image.Height % squareSize != 0)
            {
                throw new ArgumentException(Constantes.ERROR_SIZE_SQUARE);
            }

            using (Graphics g = Graphics.FromImage(image))
            {
                for (int x = 0; x < image.Width; x += squareSize)
                {
                    for (int y = 0; y < image.Height; y += squareSize)
                    {
                        Color squareColor = _usefulFunctions.GetColorFromArray(byte_array, indexArray);
                        indexArray += 3;
                        // Dessiner le carré avec la couleur déterminée
                        _graphics.FillRectangle(new SolidBrush(squareColor), new Rectangle(x, y, squareSize, squareSize));
                    }
                }
                // TODO: dans le cas ou le byte array est plus grande la taille de l'image cree une autre image et ainsi de suite ca se fera dans une autre class
            }
        }

        public byte[] ReadImageBySquares(Bitmap image, int squareSize, int byteLength)
        {
            byte[] byteArray = new byte[byteLength];
            int indexArray = 0;

            using (Bitmap clone = new Bitmap(image))
            {
                for (int x = 0; x < image.Width; x += squareSize)
                {
                    for (int y = 0; y < image.Height; y += squareSize)
                    {
                        // Vérifier si l'index est en dehors des limites du tableau
                        if (indexArray >= byteLength)
                            return byteArray;
                        // Lire la couleur du pixel central du carré
                        Color squareColor = clone.GetPixel(x, y);
                        byteArray = _usefulFunctions.GetByteFromColor(byteArray, indexArray, squareColor);
                        indexArray += 3;
                    }
                }
            }

            return byteArray;
        }
    }
}
