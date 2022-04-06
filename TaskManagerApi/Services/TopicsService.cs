using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public interface ITopicsService
    {
        Task AddTopic(Topic model);
        Task GeneratTopic();
    }


    public class TopicsService : ITopicsService
    {
        private readonly ApplicationContext _context;

        public TopicsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task GeneratTopic()
        {
            var topicModel = new Topic
            {
                Id = 44,
                Name = "123",
                ParentId = null
            };
            await AddTopic(topicModel);
            return;
        }

        public async Task AddTopic(Topic model)
        {
            await _context.Topics.AddAsync(new Topic
            {
                Id = model.Id,
                Name = model.Name,
                ParentId = model.ParentId
            });
            await _context.SaveChangesAsync();
        }


    }
}
