using System.Collections.Generic;

namespace WorkersFeature.Dtos
{
    public class PersonLessInfoDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SkillDto> Skills { get; set; }
    }
}