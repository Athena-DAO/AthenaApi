using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandAndControlWebApi.ViewModels;
using System.IO;
using Microsoft.AspNetCore.SignalR;
using CommandAndControlWebApi.Hubs;
using CommandAndControlWebApi.Services;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Logging")]
    public class LoggingController : Controller
    {
        private IHubContext<LogHub> hubContext;

        public LoggingController(IHubContext<LogHub> hubContext)
        {
            this.hubContext = hubContext;
        }

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
        public async Task<IActionResult> Post([FromBody]PipelineLogViewModel value)
        {
            string targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", value.PipelineId) + ".txt";
            System.IO.File.AppendAllText(targetFilePath, value.Log);
            string connectionId = LoggingService.GetUser(value.PipelineId);
            if (connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("Log", value.Log);
            }
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
