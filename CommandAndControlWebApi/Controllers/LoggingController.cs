using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandAndControlWebApi.ViewModels;
using System.IO;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Logging")]
    public class LoggingController : Controller
    {
        // GET: api/Logging
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Logging/5
        [HttpGet("{id}", Name = "GetLogs")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Logging
        [HttpPost]
        public IActionResult Post([FromBody]PipelineLogViewModel value)
        {
            string targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", value.PipelineId) + ".txt";
            System.IO.File.AppendAllText(targetFilePath, value.Log);
            return Ok();
        }
        
        // PUT: api/Logging/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
