using System.Linq;
using Metsys.Validate.Validators;
using Xunit;

namespace Metsys.Validate.Tests
{
    public class ValueEqualityValidatorTests
    {
        [Fact]
        public void IsValidWhenNullComparedToNull()
        {
            Assert.Equal(true, new ValueEqualityValidator(null).IsValid(null, null));
        }
        [Fact]
        public void IsInvalidWhenNullComparedToNonNull()
        {
            Assert.Equal(false, new ValueEqualityValidator("abc").IsValid(null, null));
        }
        [Fact]
        public void IsValidWhenTwoStringsAreEqual()
        {
            Assert.Equal(true, new ValueEqualityValidator("same").IsValid("same", null));
        }
        [Fact]
        public void IsInvalidWhenTwoStringsAreNotEqual()
        {
            Assert.Equal(false, new ValueEqualityValidator("something else").IsValid("null", null));
        }
        [Fact]
        public void IsValidWhenTwoIntsAreEqual()
        {
            Assert.Equal(true, new ValueEqualityValidator(58).IsValid(58, null));
        }
        [Fact]
        public void IsInvalidWhenTwoIntsAreNotEqual()
        {
            Assert.Equal(false, new ValueEqualityValidator(58).IsValid(95, null));
        }

        [Fact]
        public void JsonValueEscapesStrings()
        {
            Assert.Equal(1, new ValueEqualityValidator("over9000").ToJson().Count(kvp => kvp.Key == "eq" && kvp.Value == "'over9000'"));
        }

        [Fact]
        public void JsonValueDoesNotEscapeNumber()
        {
            Assert.Equal(1, new ValueEqualityValidator(54).ToJson().Count(kvp => kvp.Key == "eq" && kvp.Value == "54"));
        }
        
        
    }
}