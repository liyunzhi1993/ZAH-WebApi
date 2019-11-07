using System;
using System.Collections.Generic;
using System.Text;

namespace zah_core.AttributeTag
{
    public class TextAttribute : Attribute
    {
        public TextAttribute(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
