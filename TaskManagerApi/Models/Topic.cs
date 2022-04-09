namespace TaskManagerApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ICollection<Topic> TopicChildsId { get; set; }
    }
}
