using System;

namespace Antwiwaa.ArchBit.Shared.Common.Attributes
{
    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }

        public string StringValue { get; set; }
    }
}