using LessonMonitor.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LessonMonitor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private static readonly string[] secondScills = new[]
        {
            "Music", "Painting", "Sport", "TvShow", "Modelling", "Fishing"
        };

        private static readonly string[] Name = new[]
        {
            "Sergey", "Maria", "Ivan", "Mihail", "Svetlana", "Anton", "Irina"
        };

        public SkillsController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var rnd = new Random();
            var skills = new List<Skills>();

            for (int i = 0; i < Name.Length; i++)
            {
                var skill = new Skills
                {
                    Name = Name[rnd.Next(Name.Length)],
                    Grid = rnd.Next(1, 5),
                    SecondSkills = secondScills[rnd.Next(secondScills.Length)]
                };
                skills.Add(skill);
            }
            return Ok(skills);
        }

        [HttpGet("TypesName")]
        public IActionResult GetTypeInNamespace()
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "LessonMonitor.Api.Models").ToList();
            var typeNames = new List<string>();
            type.ForEach(x => 
            {
                typeNames.Add(x.Name);
            });

            return Ok(typeNames.ToArray());
        }

        [HttpGet("PropertyName")]
        public IActionResult GetPropertyName()
        {
            var type = typeof(Skills);
            var propertiesInformation = new List<Property>(); 
            foreach (var property in type.GetProperties())
            {
                var customAttribute = property.GetCustomAttribute<DescriptionAttribute>().Text;
                propertiesInformation.Add(new Property
                {
                    Type = property.PropertyType.ToString(),
                    Name = property.Name,
                    Description = customAttribute
                });
            }
            return Ok(propertiesInformation.ToArray());
        }

        [HttpGet("Model")]
        public void GetModel([FromQuery]Skills skills)
        {
            var type = skills.GetType();
            foreach (var property in type.GetProperties())
            {
                var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();

                if (rangeAttribute != null)
                {
                    var value = property.GetValue(skills);

                    var isValueNotRange = value is int intValue && (intValue < rangeAttribute.MinValue || intValue > rangeAttribute.MaxValue);

                    if (isValueNotRange)
                    {
                        throw new Exception($"{property.Name} : {value} is not range ({rangeAttribute.MinValue}, {rangeAttribute.MaxValue})");
                    }
                }
            }
        }
    }
}
