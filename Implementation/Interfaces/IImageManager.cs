using System.Drawing;
using System.Drawing.Imaging;

namespace Implementation.Interfaces
{
    public interface IImageManager
    {
        void SaveImage(string filePath, Bitmap image);
        Bitmap CreateImageFromBytes(byte[] imageBytes);
        byte[] GetBytesFromImage(Bitmap image, ImageFormat format);
    }
}
