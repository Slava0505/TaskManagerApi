namespace TaskManagerApi.PatchDto
{
    public class PatchTopicDto : PatchDtoBase
    {
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}
