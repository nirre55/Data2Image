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

        }
    }
}
