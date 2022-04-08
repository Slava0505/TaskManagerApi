using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;
<<<<<<< HEAD
using TaskManagerApi.ViewModels;
=======
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12

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


<<<<<<< HEAD
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
        public async Task<IActionResult> Post(TopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "Topic model is incorrect!");
            }
            try
            {
                await _topicsService.AddTopic(model);
                return Ok(model);
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
            try
            {
                var topic = await _topicsService.GetTopicViewModel(id);
                return Ok(topic);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }
=======

        // POST api/<TopicsController>
        [HttpPost]
        public async Task Post()
        {
            await _topicsService.GeneratTopic();
        }

        // GET: api/<TopicsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TopicsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
        }



        // PUT api/<TopicsController>/5
<<<<<<< HEAD
        [HttpPut("{id:int}")]
=======
        [HttpPut("{id}")]
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TopicsController>/5
<<<<<<< HEAD
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var existe = await _topicsService.GetTopic(id);

                if (existe == null)
                {
                    return NotFound();
                }
                await _topicsService.DeleteTopic(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Something went wrong");
            }

=======
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
        }
    }
}
