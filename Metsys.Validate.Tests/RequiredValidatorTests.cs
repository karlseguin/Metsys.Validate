using Xunit;
using System.Linq;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Tests
{
    public class RequiredValidatorTests
    {
        [Fact]
        public void ANullValueIsInvalid()
        {
            Assert.False(new RequiredValidator().IsValid(null, null));
        }
        [Fact]
        public void AnEmptyStringIsInvalid()
        {
            Assert.False(new RequiredValidator().IsValid(string.Empty, null));
        }
        [Fact]
        public void AnIntegerIsValid()
        {
            Assert.True(new RequiredValidator().IsValid(0, null));
            Assert.True(new RequiredValidator().IsValid(12, null));
        }
        [Fact]
        public void AStringIsValid()
        {
            Assert.True(new RequiredValidator().IsValid("abc", null));
        }

        [Fact]
        public void ProperPropertiesAreReturnedForJson()
        {
            Assert.Equal("required", new RequiredValidator().ToJson().First().Key);
            Assert.Equal("true", new RequiredValidator().ToJson().First().Value);
        }
    }
}