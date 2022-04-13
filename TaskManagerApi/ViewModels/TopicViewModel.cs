namespace TaskManagerApi.ViewModels
{
    public class BaseTopicViewModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; } 
    }
    public class TopicViewModel : BaseTopicViewModel
    {
        public int Id { get; set; }
    }

}
