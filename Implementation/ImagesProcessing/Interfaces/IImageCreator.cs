using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Implementation.ImagesProcessing.Interfaces
{
    public interface IImageCreator
    {
        Image CreateImage(int width, int height);
        void SaveImage(Image image, string filePath);
        Image<Rgba32> LoadImage(string filePath);
    }
}
