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
    [Route("api/AlgorithmParamertes")]
    public class AlgorithmParamertesController : Controller
    {
        private DataCenterContext dataCenterContext;

        public AlgorithmParamertesController(DataCenterContext dataCenterContext)
        {
            this.dataCenterContext = dataCenterContext;
        }

        // GET: api/AlgorithmParamertes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AlgorithmParamertes/5
        [HttpGet("{id}", Name = "GetAlgorithmParameters")]
        public IEnumerable<AlgorithmParameterViewModel> Get(string id)
        {
            var _id = Guid.Parse(id);
            return dataCenterContext.AlgorithmParameters.Where(x => x.Algorithm.Id == _id)
                .Select(x => new AlgorithmParameterViewModel
                {
                    Algorithm = x.Algorithm.Id.ToString(),
                    DataType = x.DataType,
                    Id = x.Id.ToString(),
                    Description = x.Description,
                    Name = x.Name
                }).ToList();
        }
        
        // POST: api/AlgorithmParamertes
        [HttpPost]
        public IActionResult Post([FromBody]AlgorithmParameterViewModel value)
        {
            Algorithm algorithm = dataCenterContext.Algorithms.Find(Guid.Parse(value.Algorithm));
            AlgorithmParameters parameters = new AlgorithmParameters
            {
                Algorithm = algorithm,
                DataType = value.DataType,
                Description = value.Description,
                Id = Guid.NewGuid(),
                Name = value.Name
            };
            dataCenterContext.AlgorithmParameters.Add(parameters);
            dataCenterContext.SaveChanges();
            return Ok();
        }
        
        // PUT: api/AlgorithmParamertes/5
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
