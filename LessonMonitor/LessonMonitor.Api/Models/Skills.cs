using System;

namespace LessonMonitor.Api.Models
{   
    [Desciption("User skills")]
    public class Skills
    {
        [Desciption("User name")]
        public string Name { get; set; }
        [Desciption("User grid")]
        [Range(1, 5)]
        public int Grid { get; set; }
        [Desciption("User second skills")]
        public string SecondSkills { get; set; }
    }

    public class DesciptionAttribute : Attribute
    {
        public DesciptionAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
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
