using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Applications.Commands;
using MediatR;
using Project.API.Applications.Services;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private IMediator _mediator;
        private IRecommendService _recommendService;
        public ProjectController(IMediator mediator, IRecommendService recommendService)
        {
            _mediator = mediator;
            _recommendService = recommendService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]Domain.AggregatesModel.Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            //project.UserId = UserIdentity.UserId;

            var command = new CreateProjectCommand() {
                Project =project
            };
            var result=await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("view/{projectId}")]
        public async Task<IActionResult> ViewProject(int projectId)
        {
            if (await _recommendService.IsProjectInRecommend(projectId, 1))
            {
                return BadRequest("没有查看该项目的权利");
            }
            var command = new ViewProjectCommand()
            {
                UserID=UserIdentity.UserId,
                UserName= UserIdentity.Name,
                Avatar=UserIdentity.Avatar,
                ProjectId=projectId
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
}