

using System.Drawing;

namespace Implementation.Utility.Interfaces
{
    public interface IUsefulFunctions
    {
        Color GetColorFromArray(byte[] array, int indexArray);
        byte[] GetByteFromColor(byte[] array, int indexArray, Color squareColor);
    }
}
