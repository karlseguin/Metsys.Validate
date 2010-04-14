using System.Linq;
using Metsys.Validate.Validators;
using Xunit;

namespace Metsys.Validate.Tests
{
    public class PropertyEqualityValidatorTests
    {
        [Fact]
        public void IsValidWhenNullComparedToNull()
        {
            Assert.Equal(true, new PropertyEqualityValidator<Fake>(f => f.OriginalString).IsValid(null, new Fake()));
        }
        [Fact]
        public void IsInvalidWhenNullComparedToNonNull()
        {
            Assert.Equal(false, new PropertyEqualityValidator<Fake>(f => f.OriginalString).IsValid(null, new Fake{OriginalString = "not null"}));
        }
        [Fact]
        public void IsValidWhenTwoStringsAreEqual()
        {
            Assert.Equal(true, new PropertyEqualityValidator<Fake>(f => f.OriginalString).IsValid("not null", new Fake { OriginalString = "not null" }));
        }
        [Fact]
        public void IsInvalidWhenTwoStringsAreNotEqual()
        {
            Assert.Equal(false, new PropertyEqualityValidator<Fake>(f => f.OriginalString).IsValid("null", new Fake { OriginalString = "not null" }));
        }
        [Fact]
        public void IsValidWhenTwoIntsAreEqual()
        {
            Assert.Equal(true, new PropertyEqualityValidator<Fake>(f => f.OriginalInt).IsValid(58, new Fake { OriginalInt = 58 }));
        }
        [Fact]
        public void IsInvalidWhenTwoIntsAreNotEqual()
        {
            Assert.Equal(false, new PropertyEqualityValidator<Fake>(f => f.OriginalInt).IsValid(95, new Fake { OriginalInt = 4 }));
        }
        
        [Fact]
        public void JsonValueContainsPropertyName()
        {
            Assert.Equal(1, new PropertyEqualityValidator<string>(s => s.Length).ToJson().Count(kvp => kvp.Key == "eqTo" && kvp.Value == "'Length'"));
        }
        
        
        private class Fake
        {
            public string OriginalString { get; set; }
            public string CompareString { get; set; }

            public int OriginalInt { get; set; }
            public int CompareInt { get; set; }
        }
    }
}