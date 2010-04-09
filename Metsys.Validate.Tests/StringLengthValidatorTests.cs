using Xunit;
using System.Linq;
using Metsys.Validate.Validators;

namespace Mogade.Tests.ValidatorTests
{
    public class StringLengthValidatorTests
    {
        [Fact]
        public void ANonStringIsAlwaysinvalid()
        {
            Assert.False(new StringLengthValidator(0, 100).IsValid(new object()));
        }        
        [Fact]
        public void AnEmptyStringIsValidWhenMinimumIsNull()
        {
            Assert.True(new StringLengthValidator(null, 100).IsValid(string.Empty));
        }
        [Fact]
        public void AStringShorterThanMinimumIsInvalid()
        {
            Assert.False(new StringLengthValidator(3, 4).IsValid("12"));
            Assert.False(new StringLengthValidator(2, 4).IsValid("12345"));
        }
        [Fact]
        public void AStringLongerThanMaximumIsInvalid()
        {
            Assert.False(new StringLengthValidator(2, 4).IsValid("12345"));
        }  
        [Fact]
        public void AStringWithintRangeIsValid()
        {
            Assert.True(new StringLengthValidator(2, 4).IsValid("12"));
            Assert.True(new StringLengthValidator(2, 4).IsValid("123"));
            Assert.True(new StringLengthValidator(2, 4).IsValid("1234"));
        }

        [Fact]
        public void MinimumJsonNotReturnedIfNull()
        {
            Assert.Equal(0, new StringLengthValidator(null, 4).ToJson().Count(c => c.Key == "min"));
        }
        [Fact]
        public void MinimumJsonNotReturnedIfZero()
        {
            Assert.Equal(0, new StringLengthValidator(0, 4).ToJson().Count(c => c.Key == "min"));
        }
        [Fact]
        public void MaximumJsonNotReturnedIfNull()
        {
            Assert.Equal(0, new StringLengthValidator(0, null).ToJson().Count(c => c.Key == "max"));
        }
        [Fact]
        public void ReturnsMinimumJson()
        {
            Assert.Equal(1, new StringLengthValidator(4, null).ToJson().Count(c => c.Key == "min" && c.Value == "4"));
        }
        [Fact]
        public void ReturnsMaximumJson()
        {
            Assert.Equal(1, new StringLengthValidator(null, 5).ToJson().Count(c => c.Key == "max" && c.Value == "5"));
        }
        [Fact]
        public void ReturnsBothMinimumAndMaximumJson()
        {
            var validator = new StringLengthValidator(2, 5).ToJson();
            Assert.Equal(1, validator.Count(c => c.Key == "max" && c.Value == "5"));
            Assert.Equal(1, validator.Count(c => c.Key == "min" && c.Value == "2"));
        }
    }
}