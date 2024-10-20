using Implementation.ImagesProcessing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;

namespace Implementation.Tests.ImagesProcessing
{
    public class ImageCreatorTests
    {
        private readonly ImageCreator _imageCreator;

        public ImageCreatorTests()
        {
            _imageCreator = new ImageCreator();
        }

        [Fact]
        public void CreateImage_ShouldReturnImageWithCorrectDimensions()
        {
            // Arrange
            int width = 100;
            int height = 200;

            // Act
            var image = _imageCreator.CreateImage(width, height);

            // Assert
            Assert.NotNull(image);
            Assert.IsType<Image<Rgba32>>(image);
            Assert.Equal(width, image.Width);
            Assert.Equal(height, image.Height);
        }

        [Fact]
        public void CreateImage_ShouldThrowArgumentExceptionForZeroDimensions()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _imageCreator.CreateImage(0, 100));
            Assert.Throws<ArgumentException>(() => _imageCreator.CreateImage(100, 0));
            Assert.Throws<ArgumentException>(() => _imageCreator.CreateImage(0, 0));
        }

        [Fact]
        public void SaveImage_ShouldCreateFileAtSpecifiedPath()
        {
            // Arrange
            var image = _imageCreator.CreateImage(10, 10);
            string filePath = Path.GetTempFileName() + ".png";

            // Act
            _imageCreator.SaveImage(image, filePath);

            // Assert
            Assert.True(File.Exists(filePath));

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void SaveImage_ShouldThrowExceptionForNullImage()
        {
            // Arrange
            string filePath = "test.png";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _imageCreator.SaveImage(It.IsAny<Image>(), filePath));
        }

        [Fact]
        public void SaveImage_ShouldThrowExceptionForInvalidFilePath()
        {
            // Arrange
            var image = _imageCreator.CreateImage(10, 10);
            string invalidFilePath = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _imageCreator.SaveImage(image, invalidFilePath));
        }

        [Fact]
        public void LoadImage_ShouldThrowArgumentException_WhenFilePathIsNull()
        {
            // Arrange
            string? filePath = null;

            // Act
            Action act = () => _imageCreator.LoadImage(filePath!);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("File path cannot be null or empty.*")
               .And.ParamName.Should().Be("filePath");
        }

        [Fact]
        public void LoadImage_ShouldThrowArgumentException_WhenFilePathIsEmpty()
        {
            // Arrange
            string filePath = "";

            // Act
            Action act = () => _imageCreator.LoadImage(filePath);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("File path cannot be null or empty.*")
               .And.ParamName.Should().Be("filePath");
        }

        [Fact]
        public void LoadImage_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = @"C:\temp\non_existent_image.png";

            // Act
            Action act = () => _imageCreator.LoadImage(filePath);

            // Assert
            act.Should().Throw<FileNotFoundException>();
        }

        [Fact(Skip = "Test Reel load image")] 
        public void LoadImage_ShouldLoadImage_WhenFileExists()
        {
            // Arrange
            var filePath = @"C:\temp\test_image.png"; // Assurez-vous que ce fichier existe pour le test.

            // Act
            var result = _imageCreator.LoadImage(filePath);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Image<Rgba32>>();
        }
    }
}
