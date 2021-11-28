using System;

namespace Antwiwaa.ArchBit.Shared.Common.Attributes
{
    public class StringDescriptionAttribute : Attribute
    {
        public StringDescriptionAttribute(string value)
        {
            StringDescription = value;
        }

        public string StringDescription { get; set; }
    }
}