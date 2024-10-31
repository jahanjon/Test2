using Application.Core.AppPage;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.ControllerLogic.Admin.CourseTitle.DTOs;
using static Application.ControllerLogic.Admin.CourseTitle.Create;
using static Application.ControllerLogic.Admin.CourseTitle.List;
using Application.ControllerLogic.Admin.CourseTitle;

namespace API.Controllers.Admins
{
    [ControllerName("Admin: CourseTitle")]
    [Route("Admin/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(Roles = Roles.Admin)]
    public class CourseTitleController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateDto dto)
        {
            return CommandResult(await Mediator.Send(new Command { Dto = dto }));
        }
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int courseId)
        {
            return HandleResult(await Mediator.Send(new Query {CourseId=courseId }));
        }
        [HttpPut("{courseTitleId}")]
        public async Task<IActionResult> Edit(EditDto dto, int courseTitleId)
        {
            return CommandResult(await Mediator.Send(new Edit.Command { Dto = dto, CourseTitleId = courseTitleId }));
        }
        [HttpDelete("{courseTitleId}")]
        public async Task<IActionResult> Delete(int courseTitleId)
        {
            return CommandResult(await Mediator.Send(new Delete.Command { CourseTitleId = courseTitleId }));
        }
    }
}
