﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        // GET: api/<SolutionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        // POST api/<SolutionController>
        [HttpPost("{taskId}/solution")]
        public void PostSolution([FromBody] string value, int taskId)
        {
        }
        // POST api/<SolutionController>
        [HttpPost("{taskId}/postomderation")]
        public void PostPostomderation([FromBody] string value, int taskId)
        {
        }
        
    }
}
