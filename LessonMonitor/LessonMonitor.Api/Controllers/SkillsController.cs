using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
    }
}
