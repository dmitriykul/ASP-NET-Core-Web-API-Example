using System.Collections.Generic;

namespace WorkersFeature.Models
{
    public class Person
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        
        public virtual List<Skill> Skills { get; set; }
    }
}