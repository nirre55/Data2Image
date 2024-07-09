using Implementation.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;

namespace Implementation
{
    public class ImageManager : IImageManager
    {
        public Bitmap CreateImageFromBytes(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }

        public byte[] GetBytesFromImage(Bitmap image, ImageFormat format)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(string filePath, Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
