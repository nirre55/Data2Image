using Implementation;
using Implementation.Interfaces;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IFileByteReader _fileByteReader;
        public Form1(IFileByteReader fileByteReader)
        {
            _fileByteReader = fileByteReader;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\test.mkv";
            var result = _fileByteReader.ReadFileBytes(filePath);
            double ss = result.Length / 3.0;
        }
    }
}
