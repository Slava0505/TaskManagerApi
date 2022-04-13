
﻿using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.PatchDto;


namespace TaskManagerApi.Services
{
    public interface ITopicsService
    {

        Task<TopicViewModel> AddTopic(BaseTopicViewModel model);
        Task PatchTopic(int id, [FromBody] PatchTopicDto patchTopicDto);
        Task<List<TopicViewModel>> GetTopicsList();
        Task<Topic?> GetTopic(int id);
        Task<TopicViewModel> GetTopicViewModel(int id);
        Task DeleteTopic(int id);
        Task PatchTopicChilds(int id, List<int> childIds);
        Task<ICollection<TopicViewModel>> GetTopicChilds(int id);
        Task DeleteTopicChilds(int id, List<int> childIds);
    }


    public class TopicsService : ITopicsService
    {
        private readonly ApplicationContext _context;

        public TopicsService(ApplicationContext context)
        {
            _context = context;
        }



        public async Task<TopicViewModel> AddTopic(BaseTopicViewModel model)
        {
            var topic = new Topic
            {
                Name = model.Name,
                ParentId = model.ParentId
            };
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();


            return await GetTopicViewModel(topic.Id);
        }
        public async Task PatchTopic(int id, PatchTopicDto patchTopicDto)
        {
            var topic = await GetTopic(id);
            // could be as well automated with smth like Automapper if you'd like to
            topic.Name = patchTopicDto.IsFieldPresent(nameof(topic.Name)) ? patchTopicDto.Name : topic.Name;
            topic.ParentId = patchTopicDto.IsFieldPresent(nameof(topic.ParentId)) ? patchTopicDto.ParentId : topic.ParentId;
            _context.Topics.Update(topic);
            // _context.Entry(topic).State = EntityState.Modified;
            await _context.SaveChangesAsync();  
        }

        public async Task<Topic?> GetTopic(int id)
        {
            return await _context.Topics.Where(x => x.Id == id).Include(x => x.ChildTopics).FirstOrDefaultAsync();
        }

        public async Task<TopicViewModel> GetTopicViewModel(int id)
        {
            var topic = await GetTopic(id);
            return new TopicViewModel
            {
                Id = topic.Id,
                Name = topic.Name,
                ParentId = topic.ParentId
            };
        }

        public async Task<List<TopicViewModel>> GetTopicsList()
        {
            var topics = await _context.Topics.Select(topic => new TopicViewModel()
            {
                Id = topic.Id,
                Name = topic.Name,
                ParentId = topic.ParentId
            }).ToListAsync();
            return topics;
        }

        public async Task DeleteTopic(int id)
        {
            var topic = await GetTopic(id);
            foreach (var child in topic.ChildTopics.ToList()) 
            {
                _context.Topics.Remove(child); 
            }
                
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
        }

        public async Task PatchTopicChilds(int id, List<int> childIds)
        {
            var parentTopic = await GetTopic(id);

            foreach (var childId in childIds)
            {
                var topic = await GetTopic(childId);
                topic.ParentId = id;
                _context.Topics.Update(topic);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TopicViewModel>> GetTopicChilds(int id)
        {
            var topic = await GetTopic(id);
            var childs = topic.ChildTopics.Select(topic => new TopicViewModel()
            {
                Id = topic.Id,
                Name = topic.Name,
                ParentId = topic.ParentId
            }).ToList();

            return childs;
        }
        public async Task DeleteTopicChilds(int id, List<int> childIds)
        {
            var topic = await GetTopic(id);
            foreach (var child in topic.ChildTopics.ToList())
            {
                if (childIds.Contains(child.Id))
                {
                    _context.Topics.Remove(child);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
