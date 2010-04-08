using Xunit;
using System.Linq;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Tests
{    
    public class CannedPatternValidatorTests
    {
        [Fact]
        public void PropertyJsonPropertiesForEmail()
        {
            Assert.Equal(1, new CannedPatternValidator(ValidationPattern.Email).ToJson().Count(kvp => kvp.Key == "email" && kvp.Value == "true"));            
        }
        [Fact]
        public void PropertyJsonPropertiesForCreditCard()
        {
            Assert.Equal(1, new CannedPatternValidator(ValidationPattern.CreditCard).ToJson().Count(kvp => kvp.Key == "creditcard" && kvp.Value == "true"));
        }
        [Fact]
        public void PropertyJsonPropertiesForUrl()
        {
            Assert.Equal(1, new CannedPatternValidator(ValidationPattern.Url).ToJson().Count(kvp => kvp.Key == "url" && kvp.Value == "true"));
        }
        [Fact]
        public void PropertyJsonPropertiesForNumber()
        {
            Assert.Equal(1, new CannedPatternValidator(ValidationPattern.Number).ToJson().Count(kvp => kvp.Key == "number" && kvp.Value == "true"));
        }
        [Fact]
        public void PropertyJsonPropertiesForDigits()
        {
            Assert.Equal(1, new CannedPatternValidator(ValidationPattern.Digits).ToJson().Count(kvp => kvp.Key == "digits" && kvp.Value == "true"));
        }
    }
}