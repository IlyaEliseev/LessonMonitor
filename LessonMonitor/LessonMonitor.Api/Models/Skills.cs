using System;

namespace LessonMonitor.Api.Models
{
    [Description("User skills")]
    public class Skills
    {
        [Description("User name")]
        public string Name { get; set; }
        [Description("User grid")]
        [Range(1, 5)]
        public int Grid { get; set; }
        [Description("User second slill")]
        public string SecondSkills { get; set; }
    }

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string text)
        {
            Text = text;
        }

        public string Text{ get; set; }
    }

    public class RangeAttribute : Attribute
    {
        public RangeAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
