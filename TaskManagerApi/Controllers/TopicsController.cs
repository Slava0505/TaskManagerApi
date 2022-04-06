using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;

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
        }



        // PUT api/<TopicsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TopicsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
