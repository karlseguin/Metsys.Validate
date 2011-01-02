(function($) 
{
    $.fn.validator = function(options) 
    {        
        var opts = $.extend($.fn.validator.defaults, options);
        var rules = opts.rules;

        return this.each(function() 
        {
            if (this.validator) { return false; }
            var $form = $(this);
            var v =
            {
                $fields: $(':text,:password,select,textarea', $form),
                checkboxes: new Array(),
                initialize: function()
                {
                    if (opts.init)
                    {
                        v.initializeInvalidFields();
                    }
                    $(':checkbox', $form).each(function(i, field)
                    {
                        var name = field.name;
                        if ($.inArray(name, v.checkboxes) == -1) { v.checkboxes.push(name);}
                    });
                    
                    v.$fields.each(function(i, field)
                    {
                        var $field = $(field);
                        $field.blur(function()
                        {          
                           if ($field.hasClass('error')) { v.validateField($field); }
                        });
                    });
                    $form.submit(function()
                    {                    
                        var isValid = true;
                        v.$fields.each(function(i, field)
                        {                           
                            var $field = $(field);                            
                            if (!v.validateField($field) && isValid)
                            {                            
                                isValid = false;
                                $field.focus();
                            }
                        });
                        for(var i = 0; i < v.checkboxes.length; ++i)
                        {
                            if (!v.validateCheckbox(v.checkboxes[i]) && isValid)
                            {
                                isValid = false;
                            }
                        }
                        return isValid;
                    });
                },
                initializeInvalidFields: function()
                {
                    for(var name in opts.init)
                    {
                        v.markAsInvalid($('[name=' + name +']', $form), opts.init[name]);
                    }
                },    
                validateCheckbox: function(name)
                {
                    var ruleList = rules[name];
                    if (!ruleList){return true;}   
                    for(var i = 0; i < ruleList.length; ++i)
                    {
                        if (!v.validateCheckboxesWithRule(name, ruleList[i]))
                        {
                            return false;
                        }
                    }
                    return true;                
                },
                validateField: function($field) 
                {
                    var ruleList = rules[$field.attr('name')];
                    if (!ruleList){return true;}   
                    for(var i = 0; i < ruleList.length; ++i)
                    {
                        if (!v.validateFieldWithRule($field, ruleList[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                },
                validateFieldWithRule: function($field, rule)
                {
                    if (!rule || $field.attr('disabled')) { return true; }
                    var value = $field.val();
                    var isValid = true;
                    if (rule.required && value.length == 0) { isValid = false; }
                    else if (!rule.required && value.length == 0 && !rule.eqTo) { isValid = true; }
                    else if (rule.min && rule.min > value.length) { isValid = false; }
                    // contributed by Scott Gonzalez: http://projects.scottsplayground.com/iri/
                    else if (rule.email) { isValid = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(value); }
                    // contributed by Scott Gonzalez: http://projects.scottsplayground.com/iri/                    
                    else if (rule.url) { isValid = /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value); }
                    else if (rule.number) { isValid = /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(value); }
                    else if (rule.digits) { isValid = /^\d+$/.test(value); }
                    else if (rule.creditcard) { isValid = v.validateCreditCard(value); }
                    else if (rule.regex) { isValid = rule.regex.test(value); }
                    else if (rule.eqTo) { isValid = $('[name$=' + rule.eqTo + ']', $form).val() == value; }
                    else if (rule.eq) { isValid = value == rule.eq; }
                    else if (rule.bexc) { isValid = value > rule.bexc[0] && value < rule.bexc[1]; }
                    else if (rule.binc) { isValid = value >= rule.bexc[0] && value <= rule.bexc[1]; }
                                                                                
                    if (!isValid) { v.markAsInvalid($field, rule.message); }
                    else { v.markAsValid($field); }
                    return isValid;
                },
                validateCheckboxesWithRule: function(name, rule)
                {
                    var values = new Array();   
                    var $checkboxes = $('[name=' + name +']', $form);
                    $checkboxes.filter(':checked').each(function(i, field)
                    {
                        values.push(field.value);
                    });                
                    var isValid = true;
                    if (rule.eq) { isValid = $.inArray(rule.eq, values) != -1; }
                    
                    if (!isValid) { v.markAsInvalid($checkboxes.filter(':first'), rule.message); }
                    else { v.markAsValid($checkboxes.filter(':first')); }
                    return isValid;
                },
        		//based on http://en.wikipedia.org/wiki/Luhn
                validateCreditCard: function(value)
                {
           			if (/[^0-9-]+/.test(value)) { return false; }

           			var nCheck = 0;
           			var nDigit = 0;
           			var bEven = false;
                    value = value.replace(/\D/g, '');                
        			for (var n = value.length - 1; n >= 0; n--) 
        			{
        				var cDigit = value.charAt(n);
        				var nDigit = parseInt(cDigit, 10);
        				if (bEven) 
        				{
        					if ((nDigit *= 2) > 9) { nDigit -= 9; }
        				}
        				nCheck += nDigit;
        				bEven = !bEven;
        			}        
        			return (nCheck % 10) == 0;                
                },
                markAsInvalid: function($field, message) 
                {                   
                    var $tip = $field.siblings('.error');
                    if ($tip.length == 0 && message) 
                    {
                        $tip = $('<label>').addClass('error').attr('for', $field.attr('name'));                        
                        $field.after($tip);
                    }
                    $tip.text(message);
                    $field.addClass('error');
                    if (opts.errorOnParent) { $field.parent().addClass($field.is(':checkbox') ? 'checkError' : 'error'); }
                    $tip.show();
                },
                markAsValid: function($field) 
                {
                    $field.siblings('.error').remove();
                    $field.removeClass('error');
                    if (opts.errorOnParent) { $field.parent().removeClass($field.is(':checkbox') ? 'checkError' : 'error'); }                    
                }
            }
            this.validator = v;
            v.initialize();
        });
    };
})(jQuery);

$.fn.validator.defaults = 
{
    showTip: true, 
    errorOnParent: false
};