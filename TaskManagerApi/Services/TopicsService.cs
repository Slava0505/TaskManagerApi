using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.ViewModels;

namespace TaskManagerApi.Services
{
    public interface ITopicsService
    {
        Task AddTopic(TopicViewModel model);
        Task<List<TopicViewModel>> GetTopicsList();
        Task<TopicViewModel> GetTopic(int id);
    }


    public class TopicsService : ITopicsService
    {
        private readonly ApplicationContext _context;

        public TopicsService(ApplicationContext context)
        {
            _context = context;
        }


        public async Task AddTopic(TopicViewModel model)
        {
            await _context.Topics.AddAsync(new Topic
            {
                Name = model.Name,
                ParentId = model.ParentId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<TopicViewModel> GetTopic(int id)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == id);
            return new TopicViewModel
            {
                Name = topic.Name,
                ParentId = topic.ParentId
            };
        }

        public async Task<List<TopicViewModel>> GetTopicsList()
        {
            var topics = await _context.Topics.Select(topic => new TopicViewModel()
            {
                Name = topic.Name,
                ParentId = topic.ParentId
            }).ToListAsync();
            return topics;
        }

    }
}
