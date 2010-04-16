namespace Metsys.Validate
{
    using System;

    public interface ISharedRuleConfiguration
    {
        ISharedRuleConfiguration WithMessage(string message);
    }
    
    public class SharedRuleConfiguration : ISharedRuleConfiguration
    {
        private readonly Action<string> _messageCallback;
        
        public SharedRuleConfiguration(Action<string> messageCallback)
        {
            _messageCallback = messageCallback;
        }

        public ISharedRuleConfiguration WithMessage(string message)
        {
            _messageCallback(message);
            return this;
        }
    }
}