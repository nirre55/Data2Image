using System.Drawing;
using Xunit;
using Implementation.Utility;

namespace Implementation.Tests.Utility
{
    public class UsefulFunctionsTests
    {
        private readonly UsefulFunctions _usefulFunctions;

        public UsefulFunctionsTests()
        {
            _usefulFunctions = new UsefulFunctions();
        }

        [Fact]
        public void GetColorFromArray_ShouldReturnColor_WhenArrayIsValid()
        {
            // Arrange
            byte[] byteArray = new byte[] { 255, 128, 64 };  // Rouge, Vert, Bleu
            int indexArray = 0;

            // Act
            Color result = _usefulFunctions.GetColorFromArray(byteArray, indexArray);

            // Assert
            Color expectedColor = Color.FromArgb(255, 128, 64);
            Assert.Equal(expectedColor, result);
        }

        [Fact]
        public void GetColorFromArray_ShouldReturnColorWithZeroBlue_WhenArrayIsTooShortForBlue()
        {
            // Arrange
            byte[] byteArray = new byte[] { 255, 128 };  // Rouge, Vert seulement
            int indexArray = 0;

            // Act
            Color result = _usefulFunctions.GetColorFromArray(byteArray, indexArray);

            // Assert
            Color expectedColor = Color.FromArgb(255, 128, 0); // Bleu est 0 par défaut
            Assert.Equal(expectedColor, result);
        }

        [Fact]
        public void GetColorFromArray_ShouldReturnColorWithZeroGreenAndBlue_WhenArrayIsTooShortForGreenAndBlue()
        {
            // Arrange
            byte[] byteArray = new byte[] { 255 };  // Rouge seulement
            int indexArray = 0;

            // Act
            Color result = _usefulFunctions.GetColorFromArray(byteArray, indexArray);

            // Assert
            Color expectedColor = Color.FromArgb(255, 0, 0); // Vert et Bleu sont 0 par défaut
            Assert.Equal(expectedColor, result);
        }

        [Fact]
        public void GetColorFromArray_ShouldReturnBlack_WhenIndexIsOutOfBounds()
        {
            // Arrange
            byte[] byteArray = new byte[] { 255, 128, 64 };  // Tableau de taille 3
            int indexArray = 3;  // Index est hors limites

            // Act
            Color result = _usefulFunctions.GetColorFromArray(byteArray, indexArray);

            // Assert
            Assert.Equal(Color.Black, result);
        }

        [Fact]
        public void GetColorFromArray_ShouldReturnBlack_WhenIndexIsNegative()
        {
            // Arrange
            byte[] byteArray = new byte[] { 255, 128, 64 };  // Tableau de taille 3
            int indexArray = -1;  // Index négatif

            // Act
            Color result = _usefulFunctions.GetColorFromArray(byteArray, indexArray);

            // Assert
            Assert.Equal(Color.Black, result);
        }

        [Fact]
        public void GetByteFromColor_ShouldAssignColorsCorrectly()
        {
            // Arrange
            byte[] byteArray = new byte[3];
            int indexArray = 0;
            Color color = Color.FromArgb(255, 128, 64); // Rouge = 255, Vert = 128, Bleu = 64

            // Act
            byte[] result = _usefulFunctions.GetByteFromColor(byteArray, indexArray, color);

            // Assert
            Assert.Equal(255, result[0]); // Vérifier rouge
            Assert.Equal(128, result[1]); // Vérifier vert
            Assert.Equal(64, result[2]);  // Vérifier bleu
        }

        [Fact]
        public void GetByteFromColor_ShouldHandleArrayLimitForGreen()
        {
            // Arrange
            byte[] byteArray = new byte[2]; // Taille limitée à 2 bytes
            int indexArray = 0;
            Color color = Color.FromArgb(255, 128, 64); // Rouge = 255, Vert = 128, Bleu = 64

            // Act
            byte[] result = _usefulFunctions.GetByteFromColor(byteArray, indexArray, color);

            // Assert
            Assert.Equal(255, result[0]); // Vérifier rouge
            Assert.Equal(128, result[1]); // Vérifier vert
            // Bleu ne doit pas être assigné car le tableau est trop petit
        }

        [Fact]
        public void GetByteFromColor_ShouldHandleArrayLimitForRedOnly()
        {
            // Arrange
            byte[] byteArray = new byte[1]; // Taille limitée à 1 byte
            int indexArray = 0;
            Color color = Color.FromArgb(255, 128, 64); // Rouge = 255, Vert = 128, Bleu = 64

            // Act
            byte[] result = _usefulFunctions.GetByteFromColor(byteArray, indexArray, color);

            // Assert
            Assert.Equal(255, result[0]); // Seul rouge doit être assigné
        }

        [Fact]
        public void GetByteFromColor_ShouldNotAssignWhenIndexExceedsArrayLength()
        {
            // Arrange
            byte[] byteArray = new byte[3]; // Tableau de 3 bytes
            int indexArray = 3; // Index en dehors de la taille du tableau
            Color color = Color.FromArgb(255, 128, 64);

            // Act
            byte[] result = _usefulFunctions.GetByteFromColor(byteArray, indexArray, color);

            // Assert
            Assert.Equal(byteArray, result); // Aucun changement ne doit être fait au tableau
        }
    }
}
