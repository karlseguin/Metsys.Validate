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

        [Fact]
        public void InvalidEmailsDoNotValidate()
        {
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Email).IsValid("I'm cool", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Email).IsValid("@about.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Email).IsValid("me@me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Email).IsValid("me@.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid(new object(), null));
        }
        [Fact]
        public void ValidOrEmptyEmailsValidate()
        {
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Email).IsValid(string.Empty, null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Email).IsValid("prepare@to.laugh", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Email).IsValid("goku.ownz@your.ass", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Email).IsValid("and@you.likeitalot", null));            
        }

        [Fact]
        public void InvalidUrlDoNotValidate()
        {
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid("I'm cool", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid("@about.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid("me@me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid("me@.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Url).IsValid(new object(), null));
        }
        [Fact]
        public void ValidOrEmptyUrlsValidate()
        {
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Url).IsValid(string.Empty, null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Url).IsValid("http://goku.owns", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Url).IsValid("http://www.goku.owns", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Url).IsValid("http://goku.owns/ur?ass=true", null));            
        }

        [Fact]
        public void InvalidNumberDoNotValidate()
        {
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid("I'm cool", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid("@about.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid("me@me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid("me@.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid("43.34.23", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Number).IsValid(new object(), null));
        }
        [Fact]
        public void ValidOrEmptyNumberValidate()
        {
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid(string.Empty, null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid("0", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid("23", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid("-23", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid("-23.4", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Number).IsValid("43.343", null));
        }

        [Fact]
        public void InvalidDigitsDoNotValidate()
        {
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("I'm cool", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("@about.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("me@me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("me@.me", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("43.34.23", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid("43.34", null));
            Assert.Equal(false, new CannedPatternValidator(ValidationPattern.Digits).IsValid(new object(), null));
        }
        [Fact]
        public void ValidOrEmptyDigitsValidate()
        {
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Digits).IsValid(string.Empty, null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Digits).IsValid("0", null));
            Assert.Equal(true, new CannedPatternValidator(ValidationPattern.Digits).IsValid("23", null));            
        }
    }
}