using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace UrlShortener.Infrastructure.Tests
{
    public class AssertArgumentTests
    {
        [Fact]
        public void IsNotNull_ShouldThrowArgumentNullException_WithNullArgument()
        {
            var argumentException = Assert.Throws<ArgumentNullException>(() => AssertArgument.NotNull(null, "testName"));
            argumentException.ParamName.Should().Be("testName");
        }

        [Fact]
        public void IsNotNull_ShouldNotThrowException_WithNotNullArgument()
        {
            AssertArgument.NotNull(new DateTime(), "testName");
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_ShouldNotThrowException_WithValidArgument()
        {
            AssertArgument.NotNullOrWhiteSpace("Hello", "Name", "message");
        }

        [Theory]
        [InlineData(null, "PropertyName", "message1")]
        [InlineData(null, "Test", "message2")]
        public void IsNotNullOrWhiteSpace_ShouldThrowArgumentNullException_WhenArgumentIsNull(string argumentValue, string argumentName, string message)
        {
            var argumentNullException = Assert.Throws<ArgumentNullException>(() =>
                AssertArgument.NotNullOrWhiteSpace(argumentValue, argumentName, message));

            argumentNullException.ParamName.Should().Be(argumentName);
        }

        [Theory]
        [InlineData("", "PropertyName", "message1")]
        [InlineData(" ", "Test", "message2")]
        public void IsNotNullOrWhiteSpace_ShouldThrowArgumentException_WhenArgumentIsWhiteSpace(string argumentValue, string argumentName, string message)
        {
            var argumentException = Assert.Throws<ArgumentException>(() =>
                AssertArgument.NotNullOrWhiteSpace(argumentValue, argumentName, message));

            argumentException.ParamName.Should().Be(argumentName);
            argumentException.Message.Should().StartWith(message);
        }

        [Fact]
        public void IsNotNullOrEmpty_ShouldNotThrowException_WithValidArgument()
        {
            AssertArgument.NotNullOrEmpty(new List<string> { "Hello" }, "ExampleName", "Message example");
        }

        [Fact]
        public void IsNotNullOrEmpty_ShouldThrowArgumentNullException()
        {
            var argumentNullException = Assert.Throws<ArgumentNullException>(() =>
                AssertArgument.NotNullOrEmpty(null, "ArgumentName", "Failed"));

            argumentNullException.ParamName.Should().Be("ArgumentName");
        }

        [Fact]
        public void IsNotNullOrEmpty_ShouldThrowArgumentException()
        {
            var argumentNullException = Assert.Throws<ArgumentException>(() =>
                AssertArgument.NotNullOrEmpty(new List<string>(), "ArgumentName", "Failed1"));

            argumentNullException.ParamName.Should().Be("ArgumentName");
            argumentNullException.Message.Should().StartWith("Failed1");
        }
    }
}
