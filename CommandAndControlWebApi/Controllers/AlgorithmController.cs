using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandAndControlWebApi.ViewModels;
using CommandAndControlWebApi.DAL;
using CommandAndControlWebApi.Models;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Algorithm")]
    public class AlgorithmController : Controller
    {
        private DataCenterContext dataCenterContext;

        public AlgorithmController(DataCenterContext dataCenterContext)
        {
            this.dataCenterContext = dataCenterContext;
        }

        // GET: api/Algorithm
        [HttpGet]
        public IEnumerable<AlgorithmViewModel> Get()
        {
            return dataCenterContext.Algorithms.Select(x => new AlgorithmViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                Cover = x.Cover
            }).ToList() ;
        }

        // GET: api/Algorithm/5
        [HttpGet("{id}", Name = "GetAlgorithm")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Algorithm
        [HttpPost]
        public IActionResult Post([FromBody]AlgorithmViewModel value)
        {
            dataCenterContext.Algorithms.Add(new Algorithm
            {
                Id = Guid.NewGuid(),
                Cover = value.Cover,
                Description = value.Description,
                MasterImage = value.Master,
                SlaveImage = value.Slave,
                Name = value.Name
            });
            dataCenterContext.SaveChanges();
            return Ok();
        }
        
        // PUT: api/Algorithm/5
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
