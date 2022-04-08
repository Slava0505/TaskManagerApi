
﻿using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.ViewModels;

namespace TaskManagerApi.Services
{
    public interface ITopicsService
    {

        Task<TopicViewModel> AddTopic(TopicViewModel model);
        Task<List<TopicViewModel>> GetTopicsList();
        Task<Topic?> GetTopic(int id);
        Task<TopicViewModel> GetTopicViewModel(int id);
        Task DeleteTopic(int id);
    }


    public class TopicsService : ITopicsService
    {
        private readonly ApplicationContext _context;

        public TopicsService(ApplicationContext context)
        {
            _context = context;
        }



        public async Task<TopicViewModel> AddTopic(TopicViewModel model)
        {
            var topic = new Topic
            {
                Name = model.Name,
                ParentId = model.ParentId
            };
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return model;
        }


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
    }
}
