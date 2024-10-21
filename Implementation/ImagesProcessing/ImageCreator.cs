using Implementation.ImagesProcessing.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Implementation.ImagesProcessing
{
    public class ImageCreator : IImageCreator
    {
        public Image CreateImage(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Width and height must be positive.");
            return new Image<Rgba32>(width, height);
        }

        public void SaveImage(Image image, string filePath)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            var encoder = new PngEncoder();
            image.Save(filePath, encoder);
        }

        public Image<Rgba32> LoadImage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            // Load the image from the specified file path
            return Image.Load<Rgba32>(filePath);
        }

    }
}
