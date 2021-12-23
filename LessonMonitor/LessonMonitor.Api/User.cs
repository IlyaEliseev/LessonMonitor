using System;

namespace LessonMonitor.API
{   
    [Description("Class user")]
    public class User
    {
        [Description("User name")]
        public string Name { get; set; }
        [Description("User age")]
        [Range(1,100)]
        public int Age { get; set; }
    }

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }

    public class RangeAttribute : Attribute
    {
        public RangeAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int MinValue { get; }
        public int MaxValue { get; }

        public void Test()
        {

        }
    }

    public class RequiredAttribute : Attribute
    {
    }
}
