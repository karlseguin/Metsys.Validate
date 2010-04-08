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
            Assert.False(new RequiredValidator().IsValid(null));
        }
        [Fact]
        public void AnEmptyStringIsInvalid()
        {
            Assert.False(new RequiredValidator().IsValid(string.Empty));
        }
        [Fact]
        public void AnIntegerIsValid()
        {
            Assert.True(new RequiredValidator().IsValid(0));
            Assert.True(new RequiredValidator().IsValid(12));
        }
        [Fact]
        public void AStringIsValid()
        {
            Assert.True(new RequiredValidator().IsValid("abc"));
        }

        [Fact]
        public void ProperPropertiesAreReturnedForJson()
        {
            Assert.Equal("required", new RequiredValidator().ToJson().First().Key);
            Assert.Equal("true", new RequiredValidator().ToJson().First().Key);
        }
    }
}