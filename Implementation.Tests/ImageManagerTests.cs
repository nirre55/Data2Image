using Moq;
using AutoFixture;
using Implementation.Utility.Interfaces;
using Implementation.Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using FluentAssertions;
using Implementation.ImagesProcessing;

namespace Implementation.Tests
{
    public class ImageManagerTests
    {
        private readonly Fixture _fixture;
        private readonly ImageManager _imageManager;
        private readonly Mock<IUsefulFunctions> _usefulFunctionsMock;

        public ImageManagerTests()
        {
            _fixture = new Fixture();
            _usefulFunctionsMock = new Mock<IUsefulFunctions>();
            _imageManager = new ImageManager(_usefulFunctionsMock.Object);
        }

        [Fact]
        public void FillImageWithBytes_ShouldThrowArgumentException_WhenByteArrayIsEmpty()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var byteArray = new byte[0];
            var squareSize = 10;

            // Act
            Action act = () => _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(Constantes.ERROR_DATA_REQUIRED);
        }

        [Fact]
        public void FillImageWithBytes_ShouldThrowArgumentException_WhenSquareSizeIsInvalid()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var byteArray = new byte[3];
            var squareSize = 0;

            // Act
            Action act = () => _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(Constantes.ERROR_INVALID_SQUARE_SIZE);
        }

        [Fact]
        public void FillImageWithBytes_ShouldThrowArgumentNullException_WhenImageIsNull()
        {
            // Arrange
            Image<Rgba32>? image = null; // Image is null
            var byteArray = new byte[3]; // Example byte array
            var squareSize = 10;

            // Act
            Action act = () => _imageManager.FillImageWithBytes(image!, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("image");
        }

        [Fact]
        public void FillImageWithBytes_ShouldThrowArgumentException_WhenImageDimensionsAreNotMultipleOfSquareSize()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 105); // Height is not a multiple of squareSize
            var byteArray = new byte[300]; // Sufficient for the test
            var squareSize = 10;

            // Act
            Action act = () => _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(Constantes.ERROR_SIZE_SQUARE);
        }

        [Fact]
        public void FillImageWithBytes_ShouldFillImageCorrectly()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var byteArray = new byte[300]; // Suffisant pour remplir l'image
            var squareSize = 10;
            var expectedColor = Color.Red;

            // Setup mock
            _usefulFunctionsMock.Setup(uf => uf.GetColorFromArray(It.IsAny<byte[]>(), It.IsAny<int>()))
                                .Returns(expectedColor);

            // Act
            _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            // Assert
            _usefulFunctionsMock.Verify(uf => uf.GetColorFromArray(It.IsAny<byte[]>(), It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ReadImageBySquares_ShouldThrowArgumentException_WhenImageDimensionsAreNotMultipleOfSquareSize()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 105); // Height is not a multiple of squareSize
            var byteArray = new byte[300]; // Sufficient for the test
            var squareSize = 10;

            // Act
            Action act = () => _imageManager.ReadImageBySquares(image, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(Constantes.ERROR_SIZE_SQUARE);
        }

        [Fact]
        public void ReadImageBySquares_ShouldThrowArgumentException_WhenByteArrayIsEmpty()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var byteArray = new byte[0];
            var squareSize = 10;

            // Act
            Action act = () => _imageManager.ReadImageBySquares(image, squareSize, byteArray);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(Constantes.ERROR_DATA_REQUIRED);
        }

        [Fact]
        public void ReadImageBySquares_ShouldCallGetByteFromColor_ExactNumberOfTimes()
        {
            // Arrange
            int squareSize = 1;
            byte[] byteArray = new byte[6]; // Exemple d'un tableau de taille 6
            var image = new Image<Rgba32>(6, 6); // Image de 6x6 pixels

            // Remplir l'image avec des pixels spécifiques pour le test
            _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            byte[] expectedBytes = new byte[] { 1, 2, 3 }; // Exemple d'une réponse attendue
            _usefulFunctionsMock.Setup(u => u.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<Rgba32>()))
                .Returns((byte[] arr, int index, Rgba32 color) => expectedBytes);

            // Act
            var result = _imageManager.ReadImageBySquares(image, squareSize, byteArray);

            // Assert
            _usefulFunctionsMock.Verify(u => u.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<Rgba32>()), Times.Exactly(byteArray.Length/3));
        }

        [Fact]
        public void ReadImageBySquares_Should_Return_Correct_ByteArray()
        {
            // Arrange
            int squareSize = 1;
            byte[] byteArray = new byte[6]; // Exemple d'un tableau de taille 6
            var image = new Image<Rgba32>(6, 6); // Image de 6x6 pixels

            // Remplir l'image avec des pixels spécifiques pour le test
            _imageManager.FillImageWithBytes(image, squareSize, byteArray);

            byte[] expectedBytes = new byte[] { 1, 2, 3 }; // Exemple d'une réponse attendue
            _usefulFunctionsMock.Setup(u => u.GetByteFromColor(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<Rgba32>()))
                .Returns((byte[] arr, int index, Rgba32 color) => expectedBytes);

            // Act
            var result = _imageManager.ReadImageBySquares(image, squareSize, byteArray);

            // Assert
            result.Should().BeEquivalentTo(expectedBytes); // Vérifie que le tableau retourné correspond à ce qui est attendu
        }


        [Fact/*(Skip = "Test Reel creation/lecture image")*/]
        public void ImageManager_TestReel()
        {
            // Arrange
            int width = 1000;
            int height = 1000;
            int squareSize = 20;
            var byteArray = new byte[2500];
            string path = @"C:\temp\test_image.png";
            FillByteArrayWithRandomColors(byteArray);
            var image = new Image<Rgba32>(width, height);
            var imageManager = new ImageManager(new UsefulFunctions()); // Remplace ConsoleFileLogger par une implémentation réelle ou un mock si nécessaire

            // Act
            imageManager.FillImageWithBytes(image, squareSize, byteArray);
            image.Save(path);
            var imageLoaded = Image.Load<Rgba32>(path);
            var data = imageManager.ReadImageBySquares(imageLoaded, squareSize, byteArray);
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