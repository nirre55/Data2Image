using Implementation.Interfaces;
using System.Drawing;

namespace Implementation
{
    public class ImageManager : IImageManager
    {
        public void FillImageWithBytes(Bitmap image, int squareSize, byte[] byte_array)
        {
            int indexArray = 0;

            using (Graphics g = Graphics.FromImage(image))
            {
                for (int x = 0; x < image.Width; x += squareSize)
                {
                    for (int y = 0; y < image.Height; y += squareSize)
                    {
                        Color squareColor;

                        if (indexArray + 2 < byte_array.Length)
                        {
                            // Si nous avons assez de données pour les 3 composantes RGB
                            byte red = byte_array[indexArray];
                            byte green = byte_array[indexArray + 1];
                            byte blue = byte_array[indexArray + 2];
                            squareColor = Color.FromArgb(red, green, blue);
                            indexArray += 3;
                        }
                        else
                        {
                            // Si nous manquons de données, compléter avec du noir
                            byte red = (indexArray < byte_array.Length) ? byte_array[indexArray] : (byte)0;
                            byte green = (indexArray + 1 < byte_array.Length) ? byte_array[indexArray + 1] : (byte)0;
                            byte blue = (indexArray + 2 < byte_array.Length) ? byte_array[indexArray + 2] : (byte)0;
                            squareColor = Color.FromArgb(red, green, blue);
                            indexArray = byte_array.Length; // Pour éviter de réutiliser les mêmes bytes
                        }

                        // Dessiner le carré avec la couleur déterminée
                        g.FillRectangle(new SolidBrush(squareColor), x, y, squareSize, squareSize);
                    }
                }
            }
        }
    }
}
