namespace Metsys.Validate.Tests
{
    using System.Linq;
    using Validators;
    using Xunit;

    public class BetweenValidatorTests
    {
        [Fact]
        public void IsValidWhenNullComparedToNull()
        {
            Assert.Equal(true, new BetweenValidator(10, 11).IsValid(null, null));
        }
        [Fact]
        public void IsValidWhenWithinRange()
        {
            Assert.Equal(true, new BetweenValidator(10, 20, true).IsValid(15, null));
            Assert.Equal(true, new BetweenValidator(10, 20, false).IsValid(14, null));
        }
        [Fact]
        public void IsValidWhenInclusiveAndOnLimit()
        {
            Assert.Equal(true, new BetweenValidator(10, 20, true).IsValid(10, null));
            Assert.Equal(true, new BetweenValidator(10, 20, true).IsValid(20, null));
        }
        [Fact]
        public void IsInvalidWhenExclusiveAndOnLimit()
        {
            Assert.Equal(false, new BetweenValidator(10, 20, false).IsValid(10, null));
            Assert.Equal(false, new BetweenValidator(10, 20, false).IsValid(20, null));
        }
        [Fact]
        public void IsInvalidWhenOutsideRange()
        {
            Assert.Equal(false, new BetweenValidator(10, 20, true).IsValid(8, null));
            Assert.Equal(false, new BetweenValidator(10, 20, true).IsValid(32, null));
            Assert.Equal(false, new BetweenValidator(10, 20, false).IsValid(7, null));
            Assert.Equal(false, new BetweenValidator(10, 20, false).IsValid(33, null));            
        }
        [Fact]
        public void JsonForExclusiveOutput()
        {
            Assert.Equal(1, new BetweenValidator(10, 20, false).ToJson().Count(kvp => kvp.Key == "bexc" && kvp.Value == "[10, 20]"));
        }
        [Fact]
        public void JsonForInclusiveOutput()
        {
            Assert.Equal(1, new BetweenValidator(8, 12, true).ToJson().Count(kvp => kvp.Key == "binc" && kvp.Value == "[8, 12]"));
        }
    }
}