using DemoProject.Backend.Data;
using DemoProject.Backend.ModelDTOs;
using DemoProject.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoProject.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugReportController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _usermanager;
        private readonly ApplicationDbContext _dbContext;
        public BugReportController(UserManager<UserIdentity> userManager, ApplicationDbContext dbContext)
        {
            _usermanager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var bugs = _dbContext.Bugs.Where(entity => entity.user.id.ToString() == User.Identity.Name).ToList();
            bugs.Add(new Core.Models.BugModel()
            {
                name = "asdasd",
                state = Core.Models.EBugState.Moderate
            });

            return Content(JsonSerializer.Serialize(bugs), "application/json");
        }


    }
}
