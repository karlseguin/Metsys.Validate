using System.Linq;
using Metsys.Validate.Validators;
using Xunit;

namespace Metsys.Validate.Tests
{
    public class PropertyEqualityValidatorTests
    {
        [Fact]
        public void JsonValueContainsPropertyName()
        {
            Assert.Equal(1, new PropertyEqualityValidator<string>(s => s.Length).ToJson().Count(kvp => kvp.Key == "eqTo" && kvp.Value == "'Length'"));
        }
    }
}