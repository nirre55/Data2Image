using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Implementation.Utility.Interfaces
{
    public interface IUsefulFunctions
    {
        Color GetColorFromArray(byte[] array, int indexArray);
        byte[] GetByteFromColor(byte[] array, int indexArray, Rgba32 squareColor);
    }
}
