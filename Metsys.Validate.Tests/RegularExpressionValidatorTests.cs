using Xunit;
using System.Linq;
using Metsys.Validate.Validators;
using System.Text.RegularExpressions;

namespace Metsys.Validate.Tests
{
    public class RegularExpressionValidatorTests
    {
        [Fact]
        public void ANullValueIsValid()
        {
            Assert.Equal(true, new RegularExpressionValidator(new Regex("abc")).IsValid(null, null));
        }
        [Fact]
        public void ANonStringIsProbablyInvalid()
        {
            Assert.Equal(false, new RegularExpressionValidator(new Regex("abc")).IsValid(new object(), null));
        }
        [Fact]
        public void AValueGetsToStringed()
        {
            Assert.Equal(true, new RegularExpressionValidator(new Regex("^5$")).IsValid(5, null));
        }
        [Fact]
        public void AStringIsInvalidIfPatternDoesntMatch()
        {
            Assert.Equal(false, new RegularExpressionValidator(new Regex("^5$")).IsValid("4", null));
        }
        [Fact]
        public void JsonValueContainsIgnoreCaseAttribute()
        {
            Assert.Equal(1, new RegularExpressionValidator(new Regex("", RegexOptions.IgnoreCase)).ToJson().Count(kvp => kvp.Value == "//i"));
        }
        [Fact]
        public void JsonValueContainsMultiLineAttribute()
        {
            Assert.Equal(1, new RegularExpressionValidator(new Regex("", RegexOptions.Multiline)).ToJson().Count(kvp => kvp.Value == "//m"));
        }
        [Fact]
        public void JsonValueContainsIgnoreCaseAndMultiLineAttribute()
        {
            Assert.Equal(1, new RegularExpressionValidator(new Regex("", RegexOptions.Multiline | RegexOptions.IgnoreCase)).ToJson().Count(kvp => kvp.Value == "//im"));
        }
        [Fact]
        public void JsonValueContainsRegularExpression()
        {
            Assert.Equal(1, new RegularExpressionValidator(new Regex("^abc123$")).ToJson().Count(kvp => kvp.Key == "regex" && kvp.Value == "/^abc123$/"));
        }
    }
}