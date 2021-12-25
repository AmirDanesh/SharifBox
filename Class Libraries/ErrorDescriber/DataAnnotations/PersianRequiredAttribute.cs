using System;
using System.ComponentModel.DataAnnotations;

namespace ErrorDescriber.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class PersianRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"وارد {name} الزامی است";
        }
    }
}