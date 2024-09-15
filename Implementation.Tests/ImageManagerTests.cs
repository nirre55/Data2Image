using Moq;
using AutoFixture;
using System.Drawing;
using Implementation.Wrapper.Interfaces;
using Implementation.Utility.Interfaces;
using Implementation.Wrapper;
using Implementation.Utility;

namespace Implementation.Tests
{
    public class ImageManagerTests
    {
        private readonly Fixture _fixture;
        private readonly ImageManager _imageManager;
        private readonly Mock<IGraphics> _graphicsMock;
        private readonly Mock<IUsefulFunctions> _usefulFunctionsMock;

        public ImageManagerTests()
        {
            _fixture = new Fixture();
            _graphicsMock = new Mock<IGraphics>();
            _usefulFunctionsMock = new Mock<IUsefulFunctions>();
            _imageManager = new ImageManager(_graphicsMock.Object, _usefulFunctionsMock.Object);
        }

        [Fact]
        public void FillImageWithBytes_EmptyByteArray_ThrowsArgumentNullException()
        {
            // Arrange
            int squareSize = 2;
            var image = new Bitmap(4, 4);
            byte[] emptyArray = Array.Empty<byte>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _imageManager.FillImageWithBytes(image, squareSize, emptyArray));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void FillImageWithBytes_InvalidSquareSize_ThrowsArgumentException(int invalidSquareSize)
        {
            // Arrange
            var image = new Bitmap(4, 4);
            byte[] data = _fixture.Create<byte[]>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _imageManager.FillImageWithBytes(image, invalidSquareSize, data));
        }

        [Fact]
        public void FillImageWithBytes_ShouldThrowArgumentException_WhenImageSizeNotDivisibleBySquareSize()
        {
            // Arrange
            var image = new Bitmap(10, 10);
            var squareSize = 3;
            var byteArray = new byte[] { 255, 0, 0 };  // Byte array with some data

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _imageManager.FillImageWithBytes(image, squareSize, byteArray));  // Square size not divisible by image dimensions
        }

        [Fact]
        public void FillImageWithBytes_ShouldCallFillRectangleCorrectly()
        {
            // Arrange
            var image = new Bitmap(10, 10);
            int squareSize = 2;
            var byteArray = new byte[] { 255, 0, 0, 0, 255, 0, 0, 0, 255 }; // RGB values

            // Act
            _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            _graphicsMock.Verify(g => g.FillRectangle(It.IsAny<SolidBrush>(), It.IsAny<Rectangle>()), Times.Exactly(25));  // 10x10 image with 2x2 squares = 25 rectangles
        }

        [Fact]
        public void FillImageWithBytes_ShouldCallGetColorFromArrayCorrectly()
        {
            // Arrange
            var image = new Bitmap(4, 4);
            int squareSize = 2;
            var byteArray = new byte[] { 255, 0, 0, 0, 255, 0, 0, 0, 255 };

            // Act
            _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            _usefulFunctionsMock.Verify(c => c.GetColorFromArray(byteArray, It.IsAny<int>()), Times.Exactly(4));  // 4x4 image with 2x2 squares = 4 squares
        }

        [Fact]
        public void ReadImageBySquares_ShouldReturnCorrectByteArray()
        {
            // Arrange
            int squareSize = 1;
            int byteLength = 9; // 3 pixels, chaque pixel a 3 bytes (R, G, B)
            var expectedByteArray = new byte[] { 255, 0, 0, 0, 255, 0, 0, 0, 255 };
            Bitmap image = new Bitmap(3, 1); // Image de 3x1 pixels

            // Simuler les couleurs des pixels
            image.SetPixel(0, 0, Color.FromArgb(255, 0, 0)); // Rouge
            image.SetPixel(1, 0, Color.FromArgb(0, 255, 0)); // Vert
            image.SetPixel(2, 0, Color.FromArgb(0, 0, 255)); // Bleu

            // Simuler la fonction GetByteFromColor
            _usefulFunctionsMock
                .Setup(f => f.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<Color>())).Returns(expectedByteArray);

            // Act
            byte[] result = _imageManager.ReadImageBySquares(image, squareSize, byteLength);

            // Assert
            Assert.Equal(expectedByteArray, result);
        }

        [Fact]
        public void ReadImageBySquares_ShouldCallGetColorFromArrayCorrectly()
        {
            // Arrange
            var image = new Bitmap(4, 4);
            int squareSize = 1;
            var byteArray = new byte[] { 255, 0, 0, 0, 255, 0, 0, 0, 255 };

            // Simuler les couleurs des pixels
            image.SetPixel(0, 0, Color.FromArgb(255, 0, 0)); // Rouge
            image.SetPixel(1, 0, Color.FromArgb(0, 255, 0)); // Vert
            image.SetPixel(2, 0, Color.FromArgb(0, 0, 255)); // Bleu

            // Act
            _imageManager.ReadImageBySquares(image, squareSize, byteArray.Length);

            // Assert
            _usefulFunctionsMock.Verify(c => c.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<Color>()), Times.Exactly(3));  // 4x4 image with 2x2 squares = 4 squares
        }

        [Fact]
        public void ReadImageBySquares_ShouldHandleEmptyByteArray()
        {
            // Arrange
            int squareSize = 1;
            int byteLength = 0; // Taille du tableau de bytes est 0
            Bitmap image = new Bitmap(2, 2); // Image 2x2
            byte[] expectedByteArray = new byte[byteLength];

            // Act
            byte[] result = _imageManager.ReadImageBySquares(image, squareSize, byteLength);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ReadImageBySquares_ShouldReturnPartialByteArrayForLimitedSpace()
        {
            // Arrange
            int squareSize = 1;
            int byteLength = 3; // Seulement assez de place pour un pixel
            Bitmap image = new Bitmap(2, 2);
            byte[] expectedByteArray = new byte[] { 255, 128, 64 };


            // Simuler la couleur du premier pixel
            image.SetPixel(0, 0, Color.FromArgb(255, 128, 64));

            _usefulFunctionsMock
                .Setup(f => f.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), Color.FromArgb(255, 128, 64)))
                .Returns(expectedByteArray);

            // Act
            byte[] result = _imageManager.ReadImageBySquares(image, squareSize, byteLength);

            // Assert
            Assert.Equal(result, expectedByteArray);
        }

        [Fact (Skip = "Test Reel creation/lecture image")]
        public void ImageManager_TestReel()
        {
            // Arrange
            int width = 1000;
            int height = 1000;
            int squareSize = 20;
            var byteArray = new byte[2500];
            FillByteArrayWithRandomColors(byteArray);
            using var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            var graphicsWrapper = new GraphicsWrapper(graphics);
            var imageManager = new ImageManager(graphicsWrapper, new UsefulFunctions()); // Remplace ConsoleFileLogger par une implémentation réelle ou un mock si nécessaire

            // Act
            imageManager.FillImageWithBytes(image, squareSize, byteArray);
            string filePath = @"C:\temp\test_image.png";
            image.Save(filePath);

            var data = imageManager.ReadImageBySquares(image, squareSize, 2500);
            Assert.Equal(byteArray, data);
        }

        private void FillByteArrayWithRandomColors(byte[] byteArray)
        {
            var random = new Random();
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte)random.Next(0, 256); // Valeurs aléatoires entre 0 et 255
            }
        }

    }
}