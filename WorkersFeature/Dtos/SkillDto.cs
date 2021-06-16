namespace WorkersFeature.Dtos
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long? PersonId { get; set; }
    }
}