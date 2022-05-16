using System.ComponentModel.DataAnnotations;
using System;
namespace Entities.ValidateAttributes
{
    public class NotEmptyGuid : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Guid guidValue && guidValue != Guid.Empty) 
                return true;
            return false;
        }
    }
}
