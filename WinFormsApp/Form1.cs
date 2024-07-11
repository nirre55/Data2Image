using Implementation;
using Implementation.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IFileByteReader _fileByteReader;
        private readonly IImageManager _imageManager;
        public Form1(IFileByteReader fileByteReader, IImageManager imageManager)
        {
            _fileByteReader = fileByteReader;
            _imageManager = imageManager;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\test.mkv";
            var result = _fileByteReader.ReadFileBytes(filePath);
            var recreatedFilePath = @"E:\recreatedImage.png";



            int imageWidth = 1000; // Largeur de l'image
            int imageHeight = 1000; // Hauteur de l'image
            int squareSize = 4; // Taille de chaque carr� de pixels

            // Cr�er une nouvelle image vide
            Bitmap image = new Bitmap(imageWidth, imageHeight);

            // Appeler la fonction pour dessiner des carr�s de pixels avec des couleurs al�atoires
            FillImageWithSquares(image, squareSize, result);

            // Sauvegarder l'image dans un fichier
            image.Save(recreatedFilePath);

            // Lib�rer les ressources utilis�es par l'image
            image.Dispose();
            ReadSquareColors(image, 5, )
        }

        private static void FillImageWithSquares(Bitmap image, int squareSize, byte[] fileData)
        {
            int index = 0;

            using (Graphics g = Graphics.FromImage(image))
            {
                for (int x = 0; x < image.Width; x += squareSize)
                {
                    for (int y = 0; y < image.Height; y += squareSize)
                    {
                        // G�n�rer la couleur en utilisant les tableau de byte
                        Color color = Color.FromArgb(fileData[index], fileData[index+1], fileData[index+2]);
                        index += 3;
                        // Dessiner un carr� de pixels avec la couleur al�atoire
                        g.FillRectangle(new SolidBrush(color), x, y, squareSize, squareSize);
                    }
                }
            }
        }

        private static byte[] ReadSquareColors(Bitmap image, int squareSize, int maxBytes)
        {
            int numSquares = Math.Min((image.Width / squareSize) * (image.Height / squareSize), maxBytes / 3);
            byte[] colorArray = new byte[numSquares * 3];

            int index = 0;
            for (int x = 0; x < image.Width && index < maxBytes; x += squareSize)
            {
                for (int y = 0; y < image.Height && index < maxBytes; y += squareSize)
                {
                    // Lire la couleur du coin sup�rieur gauche de chaque carr�
                    Color squareColor = image.GetPixel(x, y);

                    // Enregistrer les valeurs RGB dans le tableau de bytes
                    colorArray[index++] = squareColor.R;
                    colorArray[index++] = squareColor.G;
                    colorArray[index++] = squareColor.B;

                    // Arr�ter si nous atteignons la taille maximale du tableau
                    if (index >= maxBytes) break;
                }
            }
            return colorArray.Take(maxBytes).ToArray();
        }
    }
}
