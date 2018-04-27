using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandAndControlWebApi.ViewModels;
using CommandAndControlWebApi.DAL;
using Microsoft.AspNetCore.Identity;
using CommandAndControlWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CommandAndControlWebApi.Services;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Pipeline")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class PipelineController : Controller
    {
        private readonly DataCenterContext dataCenterContext;
        private readonly UserManager<IdentityUser> userManager;

        public PipelineController(DataCenterContext dataCenterContext, UserManager<IdentityUser> userManager)
        {
            this.dataCenterContext = dataCenterContext;
            this.userManager = userManager;
        }

        // GET: api/Pipeline
        [HttpGet]
        public IEnumerable<PipelineViewModel> Get()
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            return dataCenterContext.ProfilePipeline
                .Where(x => x.ProfileId == _id)
                .Select(x => new PipelineViewModel
                {
                    Id = x.PipelineId.ToString(),
                    AlgorithmId = x.Pipeline.Id.ToString(),
                    AlgorithmName = x.Pipeline.Algorithm.Name,
                    AlgorithmDescription = x.Pipeline.Algorithm.Description,
                    Description = x.Pipeline.Description,
                    Name = x.Pipeline.Name,
                    NumberOfContainers = x.Pipeline.NumberOfContainers,
                    Result = x.Pipeline.Result,
                    Parameters = x.Pipeline.PipelineParameters.Select(y => new PipelineParameterViewModel
                    {
                        ParameterName = y.AlgorithmParameter.Name,
                        ParameterDescription = y.AlgorithmParameter.Description,
                        Value = y.Value
                    }).ToList()
                }).ToList();
        }

        // GET: api/Pipeline/5
        [HttpGet("{id}", Name = "GetPipeline")]
        public PipelineViewModel Get(string id)
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            var _id1 = Guid.Parse(id);
            return dataCenterContext.ProfilePipeline
                .Where(x => x.ProfileId == _id && x.PipelineId == _id1)
                .Select(x => new PipelineViewModel
                {
                    Id = x.PipelineId.ToString(),
                    AlgorithmId = x.Pipeline.Id.ToString(),
                    AlgorithmName = x.Pipeline.Algorithm.Name,
                    AlgorithmDescription = x.Pipeline.Algorithm.Description,
                    Description = x.Pipeline.Description,
                    Name = x.Pipeline.Name,
                    NumberOfContainers = x.Pipeline.NumberOfContainers,
                    Result = x.Pipeline.Result,
                    Parameters = x.Pipeline.PipelineParameters.Select(y => new PipelineParameterViewModel
                    {
                        ParameterName = y.AlgorithmParameter.Name,
                        ParameterDescription = y.AlgorithmParameter.Description,
                        Value = y.Value
                    }).ToList()
                }).First();
        }

        // POST: api/Pipeline
        [HttpPost]
        public IActionResult Post([FromBody]PipelineViewModel value)
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            Profile profile = dataCenterContext.Profiles.Where(x => x.Id == _id).First();
            Algorithm algorithm = dataCenterContext.Algorithms.Find(Guid.Parse(value.AlgorithmId));
            Pipeline pipeline = new Pipeline
            {
                Algorithm = algorithm,
                Id = Guid.NewGuid(),
                Name = value.Name,
                Description = value.Description,
                NumberOfContainers = value.NumberOfContainers
            };
            List<PipelineParameter> parameters = new List<PipelineParameter>();
            foreach (var parameter in value.Parameters)
            {
                AlgorithmParameters algorithmParameter = dataCenterContext.AlgorithmParameters.Find(Guid.Parse(parameter.Id));
                parameters.Add(new PipelineParameter
                {
                    Id = Guid.NewGuid(),
                    Pipeline = pipeline,
                    Value = parameter.Value,
                    AlgorithmParameter = algorithmParameter
                });
            }
            pipeline.PipelineParameters = parameters;
            ProfilePipeline profilePipeline = new ProfilePipeline
            {
                Pipeline = pipeline,
                Profile = profile
            };
            dataCenterContext.ProfilePipeline.Add(profilePipeline);
            dataCenterContext.SaveChanges();

            RabbitMqService rabbitMqService = new RabbitMqService();
            rabbitMqService.SendMessage(pipeline.Id + "~" + algorithm.MasterImage);
            for (int i = 0; i < pipeline.NumberOfContainers - 1; i++)
            {
                rabbitMqService.SendMessage(pipeline.Id + "~" + algorithm.SlaveImage);
            }

            return Ok();
        }

        // PUT: api/Pipeline/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("Result")]
        public IActionResult UpdateResult([FromBody]PipelineResultViewModel pipelineResultViewModel)
        {
            Pipeline pipeline = dataCenterContext.Pipelines.Find(Guid.Parse(pipelineResultViewModel.PipelineId));
            pipeline.Result = pipelineResultViewModel.Result;
            dataCenterContext.Entry(pipeline).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dataCenterContext.SaveChanges();
            return Ok();
        }
    }
}
