using FluentAssertions;
using Implementation.Wrapper;
using Newtonsoft.Json;


namespace Implementation.Tests.Wrapper
{
    public class JsonConvertWrapperTests
    {
        [Fact]
        public void SerializeObject_ShouldReturnValidJson_WhenObjectIsValid()
        {
            // Arrange
            var wrapper = new JsonConvertWrapper();
            var testObject = new { Name = "Test", Age = 30 };

            // Act
            var result = wrapper.SerializeObject(testObject, Formatting.None);

            // Assert
            result.Should().Be("{\"Name\":\"Test\",\"Age\":30}");
        }

        [Fact]
        public void DeserializeObject_ShouldReturnValidObject_WhenJsonIsValid()
        {
            // Arrange
            var wrapper = new JsonConvertWrapper();
            string validJson = "{\"Name\":\"Test\",\"Age\":30}";

            // Act
            var result = wrapper.DeserializeObject<TestObject>(validJson);

            // Assert
            result.Name.Should().Be("Test");
            result.Age.Should().Be(30);
        }

        [Fact]
        public void DeserializeObject_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            var wrapper = new JsonConvertWrapper();
            string invalidJson = "{\"Name\":\"Test\",\"Age\":}"; // JSON malformé

            // Act
            Action act = () => wrapper.DeserializeObject<dynamic>(invalidJson);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Invalid JSON data.");
        }

        [Fact]
        public void DeserializeObject_ShouldThrowInvalidOperationException_WhenDeserializationReturnsNull()
        {
            // Arrange
            var wrapper = new JsonConvertWrapper();
            string jsonWithNull = "null"; // Forcer un résultat nul

            // Act
            Action act = () => wrapper.DeserializeObject<dynamic>(jsonWithNull);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Deserialization returned null.");
        }

        public class TestObject
        {
            public string? Name { get; set; }
            public int Age { get; set; }
        }
    }

}
