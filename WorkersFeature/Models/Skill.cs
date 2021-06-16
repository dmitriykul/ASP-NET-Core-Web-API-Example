namespace WorkersFeature.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public byte Level { get; set; }
        public int? PersonId { get; set; }
        
        public virtual Person Person { get; set; }
    }
}