namespace TaskManagerApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Topic? ParentTopic { get; set; }
        public int? ParentId { get; set; }
        public ICollection<Topic> ChildTopics { get; set; }
    }
}
