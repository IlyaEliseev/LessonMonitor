using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LessonMonitor.Api.Models;
using System.Reflection;
using System.Linq;

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
        
        [HttpGet("TypesInNameSpace")]
        public IActionResult GetTypesInNamespace()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == "LessonMonitor.Api.Models").ToList();
            var typeNames = new List<string>();
            types.ForEach(x =>
            {
                typeNames.Add(x.Name);
            });

            return Ok(typeNames.ToArray());
        }

        [HttpGet("Properties")]
        public IActionResult GetModelProperties()
        {
            var type = typeof(Skills);
            var properties = type.GetProperties();
            var propertiesInformation = new List<Property>();
            foreach (var property in properties)
            {
                var customAttribute = property.GetCustomAttribute<DesciptionAttribute>().Text;

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

                    var IsValueNotInRange = value is int intValue 
                        && (intValue < rangeAttribute.MinValue 
                        || intValue > rangeAttribute.MaxValue);

                    if (IsValueNotInRange)
                    {
                        throw new Exception($"{property.Name} : {value} - not in range ({rangeAttribute.MinValue}, {rangeAttribute.MaxValue})");
                    }
                }
            }
        }
    }
}
