namespace Metsys.Validate.Validators
{    
    using System.Collections.Generic;

    public class BetweenValidator : IValidator, IDoJavascript
    {
        private readonly int _minimum;
        private readonly int _maximum;
        private readonly bool _inclusive;

        public BetweenValidator(int minimum, int maximum) : this(minimum, maximum, true) { }
        public BetweenValidator(int minimum, int maximum, bool inclusive)
        {
            _minimum = minimum;
            _maximum = maximum;
            _inclusive = inclusive;
        }

        public bool IsValid(object value, object container)
        {            
            if (value == null) { return true; }
            var typed = (int) value;
            return _inclusive ? typed >= _minimum && typed <= _maximum : typed > _minimum && typed < _maximum;
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            var between = string.Format("[{0}, {1}]", _minimum, _maximum);
            var key = _inclusive ? "binc" : "bexc";
            yield return new KeyValuePair<string, string>(key, between);
        }
    }
}