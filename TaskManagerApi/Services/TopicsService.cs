<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.ViewModels;
=======
﻿using TaskManagerApi.Models;
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12

namespace TaskManagerApi.Services
{
    public interface ITopicsService
    {
<<<<<<< HEAD
        Task AddTopic(TopicViewModel model);
        Task<List<TopicViewModel>> GetTopicsList();
        Task<Topic?> GetTopic(int id);
        Task<TopicViewModel> GetTopicViewModel(int id);
        Task DeleteTopic(int id);
=======
        Task AddTopic(Topic model);
        Task GeneratTopic();
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
    }


    public class TopicsService : ITopicsService
    {
        private readonly ApplicationContext _context;

        public TopicsService(ApplicationContext context)
        {
            _context = context;
        }

<<<<<<< HEAD

        public async Task AddTopic(TopicViewModel model)
        {
            await _context.Topics.AddAsync(new Topic
            {
=======
        public async Task GeneratTopic()
        {
            var topicModel = new Topic
            {
                Name = "123"
            };
            await AddTopic(topicModel);
            return;
        }

        public async Task AddTopic(Topic model)
        {
            await _context.Topics.AddAsync(new Topic
            {
                Id = model.Id,
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
                Name = model.Name,
                ParentId = model.ParentId
            });
            await _context.SaveChangesAsync();
        }

<<<<<<< HEAD
        public async Task<Topic?> GetTopic(int id)
        {
            return await _context.Topics.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TopicViewModel> GetTopicViewModel(int id)
        {
            var topic = await GetTopic(id);
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

        public async Task DeleteTopic(int id)
        {
            var topic = await GetTopic(id);
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            
        }
=======
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12

    }
}
