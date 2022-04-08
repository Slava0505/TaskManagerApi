using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;
using TaskManagerApi.ViewModels;
using TaskManagerApi.PatchDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {

        private readonly ILogger<TopicsController> _logger;
        private readonly ITopicsService _topicsService;
        public TopicsController(ILogger<TopicsController> logger,
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
        public async Task<IActionResult> Post(TopicViewModel topicViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "Topic model is incorrect!");
            }
            try
            {
                await _topicsService.AddTopic(topicViewModel);
                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
                
        }



        // GET api/<TopicsController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            // Checking for existence
            var topic = await _topicsService.GetTopic(id);
            if (topic == null)
            {
                return NotFound();
            }
            try
            {
                var topicViewModel = await _topicsService.GetTopicViewModel(id);
                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }



        // PUT api/<TopicsController>/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, PatchTopicDto patchTopicDto)
        {
            try
            {
                var topic = await _topicsService.GetTopic(id);

                if (topic == null)
                {
                    return NotFound();
                }

                await _topicsService.PatchTopic(id, patchTopicDto);
                var topicViewModel = await _topicsService.GetTopicViewModel(id);

                return Ok(topicViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }

        // DELETE api/<TopicsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var topic = await _topicsService.GetTopic(id);

                if (topic == null)
                {
                    return NotFound();
                }
                await _topicsService.DeleteTopic(id);
                // todo Make message
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
        }
    }
}
