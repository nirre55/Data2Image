using Implementation;
using Implementation.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;

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
            File.WriteAllBytes(recreatedFilePath, result);

            // Créer une image à partir des bytes
            var recreatedBitmap = _imageManager.CreateImageFromBytes(result);
            Console.WriteLine($"Image recreated from bytes, dimensions: {recreatedBitmap.Width}x{recreatedBitmap.Height}");

            // Sauvegarder l'image recréée
            _imageManager.SaveImage(filePath, recreatedBitmap);
            Console.WriteLine($"Recreated image saved to {recreatedFilePath}");

        }
    }
}
