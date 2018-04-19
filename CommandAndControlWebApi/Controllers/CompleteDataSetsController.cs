using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CommandAndControlWebApi.DAL;
using CommandAndControlWebApi.ViewModels;
using CommandAndControlWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CompleteDataSets")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class CompleteDataSetsController : Controller
    {
        private DataCenterContext dataCenterContext;
        private UserManager<IdentityUser> userManager;

        public CompleteDataSetsController(DataCenterContext dataCenterContext, UserManager<IdentityUser> userManager)
        {
            this.dataCenterContext = dataCenterContext;
            this.userManager = userManager;
        }

        // GET: api/CompleteDataSets
        [HttpGet]
        public IEnumerable<CompleteDataSetViewModel> Get()
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            return dataCenterContext.ProfilesCompleteDataSets.Where(x => x.ProfileId == _id)
                .Select(x => new CompleteDataSetViewModel
                {
                    Description = x.CompleteDataSet.Description,
                    Id = x.CompleteDataSet.Id.ToString(),
                    Name = x.CompleteDataSet.Name,
                    XComponentId = x.CompleteDataSet.XComponentDataSet.Id.ToString(),
                    YComponentId = x.CompleteDataSet.YComponentDataSet.Id.ToString(),
                    XComponentName = x.CompleteDataSet.XComponentDataSet.Name,
                    YComponentName = x.CompleteDataSet.YComponentDataSet.Name
                }).ToList();
        }

        // GET: api/CompleteDataSets/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CompleteDataSets
        [HttpPost]
        public IActionResult Post([FromBody]CompleteDataSetViewModel value)
        {
            var _id = Guid.Parse(userManager.GetUserId(User));
            Profile profile = dataCenterContext.Profiles.Where(x => x.Id == _id).First();
            CompleteDataSet completeDataSet = new CompleteDataSet
            {
                Id = Guid.NewGuid(),
                Description = value.Description,
                Name = value.Name,
                XComponentDataSet = dataCenterContext.DataSets.Find(Guid.Parse(value.XComponentId)),
                YComponentDataSet = dataCenterContext.DataSets.Find(Guid.Parse(value.YComponentId))
            };
            ProfileCompleteDataSet profileCompleteDataSet = new ProfileCompleteDataSet
            {
                CompleteDataSet = completeDataSet,
                Profile = profile
            };
            dataCenterContext.ProfilesCompleteDataSets.Add(profileCompleteDataSet);
            dataCenterContext.SaveChanges();
            return Ok();
        }

        // PUT: api/CompleteDataSets/5
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
