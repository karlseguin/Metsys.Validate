using Xunit;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Tests
{
    public class ConfigurationTests : BaseFixture
    {
        [Fact]
        public void ReturnsNullIfTypeIsntFound()
        {
            Assert.Equal(null, Validator.RulesFor<object>());            
        }        
        [Fact]
        public void RegistersASingleValidator()
        {
            var data = Validator.RulesFor<Fake>().Rules["Name"];
            Assert.Equal(1, data.Validators.Count);
            Assert.IsType(typeof(RequiredValidator), data.Validators[0]);
        }
        [Fact]
        public void ValidationMessageIsSet()
        {
            var data = Validator.RulesFor<Fake>().Rules["Name"];
            Assert.Equal("Fix your mess!", data.Message);
        }
        
        public class FakeValidator : ClassValidator<Fake>
        {
            public FakeValidator()
            {
                RuleFor(f => f.Name).Required().WithMessage("Fix your mess!");
            }
        }
        
        public class Fake
        {
            public int Id{ get; set;}
            public string Name{ get; set;}
        }
    }
}