﻿using System.Drawing;

namespace Implementation.Interfaces
{
    public interface IImageManager
    {
        void FillImageWithBytes(Bitmap image, int squareSize, byte[] byte_array);
        byte[] ReadImageBySquares(Bitmap image, int squareSize, int byteLength);
    }
}
