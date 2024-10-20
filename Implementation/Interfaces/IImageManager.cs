using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace Implementation.Interfaces
{
    public interface IImageManager
    {
        void FillImageWithBytes(Image image, int squareSize, byte[] byte_array);
        byte[] ReadImageBySquares(Image<Rgba32> image, int squareSize, byte[] byteArray);
    }
}
