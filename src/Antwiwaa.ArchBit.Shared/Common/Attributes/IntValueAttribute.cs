using System;

namespace Antwiwaa.ArchBit.Shared.Common.Attributes
{
    public class IntValueAttribute : Attribute
    {
        public IntValueAttribute(int value)
        {
            IntValue = value;
        }

        public int IntValue { get; set; }
    }
}