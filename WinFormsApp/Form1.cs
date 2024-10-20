using Implementation.Interfaces;
using Implementation.ImagesProcessing.Interfaces;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IFileByteReader _fileByteReader;
        private readonly IImageManager _imageManager;
        private readonly IImageCreator _imageCreator;
        public Form1(IFileByteReader fileByteReader, IImageManager imageManager, IImageCreator imageCreator)
        {
            _fileByteReader = fileByteReader;
            _imageManager = imageManager;
            _imageCreator = imageCreator;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = 100;
            int height = 100;
            int squareSize = 2;
            var byteArray = new byte[2500];
            string path = @"C:\temp\test_image.png";
            FillByteArrayWithRandomColors(byteArray);
            var image = _imageCreator.CreateImage(width, height);
            using (image)
            {
                _imageManager.FillImageWithBytes(image, squareSize, byteArray);
                _imageCreator.SaveImage(image, path);
            }
        }

        private void FillByteArrayWithRandomColors(byte[] byteArray)
        {
            var random = new Random();
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte)random.Next(0, 256); // Valeurs aléatoires entre 0 et 255
            }
        }
    }
}
