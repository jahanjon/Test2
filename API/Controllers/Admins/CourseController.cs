using Application.Core.AppPage;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.ControllerLogic.Admin.Course.DTOs;
using static Application.ControllerLogic.Admin.Course.Create;
using static Application.ControllerLogic.Admin.Course.List;
using Application.ControllerLogic.Admin.Course;

namespace API.Controllers.Admins
{
    [ControllerName("Admin: Course")]
    [Route("Admin/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(Roles = Roles.Admin)]
    public class CourseController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateDto dto)
        {
            return CommandResult(await Mediator.Send(new Command { Dto = dto }));
        }
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedListInputDto dto, [FromQuery] FilterInputDto input)
        {
            return HandleResult(await Mediator.Send(new Query { Dto = dto, Input = input }));
        }
        [HttpPut("{courseId}")]
        public async Task<IActionResult> Edit(EditDto dto, int courseId)
        {
            return CommandResult(await Mediator.Send(new Edit.Command { Dto = dto, CourseId = courseId }));
        }
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> Delete(int courseId)
        {
            return CommandResult(await Mediator.Send(new Delete.Command { CourseId = courseId }));
        }
        [HttpPatch("{courseId}")]
        public async Task<IActionResult>ChangeStatus(int courseId)
        {
            return HandleResult(await Mediator.Send(new ChnageStatue.Command { CourseId = courseId }));
        }
    }
}
