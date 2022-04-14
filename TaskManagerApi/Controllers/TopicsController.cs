﻿using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;
using TaskManagerApi.ViewModels;
using TaskManagerApi.PatchDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("topics/")]
    [ApiController]
    public class TopicsController2 : ControllerBase
    {

        private readonly ILogger<TopicsController2> _logger;
        private readonly ITopicsService _topicsService;
        public TopicsController2(ILogger<TopicsController2> logger,
                ITopicsService topicsService)
        {
            _logger = logger;
            _topicsService = topicsService; //Пробрасываем сервис в конструкторе из контейнера зависимостей
        }


        // GET: api/<TopicsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var topics = await _topicsService.GetTopicsList();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }

        // POST api/<TopicsController>
        [HttpPost]
        public async Task<IActionResult> Post(BaseTopicViewModel baseTopicViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "Topic model is incorrect!");
            }
            try
            {
                var topicViewModel = await _topicsService.AddTopic(baseTopicViewModel);
                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
                
        }



        // GET api/<TopicsController>/5
        [HttpGet("{topicId:int}")]
        public async Task<IActionResult> Get(int topicId)
        {
            // Checking for existence
            var topic = await _topicsService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound();
            }
            try
            {
                var topicViewModel = await _topicsService.GetTopicViewModel(topicId);
                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }



        // PUT api/<TopicsController>/5
        [HttpPatch("{topicId:int}")]
        public async Task<IActionResult> Patch(int topicId, PatchTopicDto patchTopicDto)
        {
            try
            {
                var topic = await _topicsService.GetTopic(topicId);

                if (topic == null)
                {
                    return NotFound();
                }

                await _topicsService.PatchTopic(topicId, patchTopicDto);
                var topicViewModel = await _topicsService.GetTopicViewModel(topicId);

                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }

        // DELETE api/<TopicsController>/5
        [HttpDelete("{topicId:int}")]
        public async Task<ActionResult> Delete(int topicId)
        {
            var topic = await _topicsService.GetTopic(topicId);

            if (topic == null)
            {
                return NotFound();
            }
            await _topicsService.DeleteTopic(topicId);
            try
            {

                // todo Make message
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }

        // GET api/<TopicsController>/5
        [HttpGet("{topicId:int}/childs")]
        public async Task<IActionResult> GetChilds(int topicId)
        {
            // Checking for existence
            var topic = await _topicsService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound();
            }

                var childs = await _topicsService.GetTopicChilds(topicId);
                return Ok(childs);

        }

        // POST api/<TopicsController>
        [HttpPost("{topicId:int}/childs")]
        public async Task<IActionResult> PostChilds(int topicId, List<int> childIds)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "Topic model is incorrect!");
            }

                await _topicsService.PatchTopicChilds(topicId, childIds);

                return Ok();

        }


        // POST api/<TopicsController>
        [HttpDelete("{topicId:int}/childs")]
        public async Task<IActionResult> DeleteChilds(int topicId, List<int> childIds)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "Topic model is incorrect!");
            }

            await _topicsService.DeleteTopicChilds(topicId, childIds);

            return Ok();

        }
    }
}
