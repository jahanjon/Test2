using Application.ControllerLogic.Public.Course;
using Application.Core.AppPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.ControllerLogic.Public.Course.List;

namespace API.Controllers.Publics
{
    [ControllerName("Public: Course")]
    [Route("Public/[controller]")]
    [ApiVersion("1.0")]
    public class CourseController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedListInputDto dto, [FromQuery] FilterInputDto input,int subCategoryId)
        {
            return HandleResult(await Mediator.Send(new Query { Dto = dto, Input = input,SubCategoryId=subCategoryId }));
        }
        [HttpGet("{url}")]
        public async Task<IActionResult> Get(string url)
        {
            return HandleResult(await Mediator.Send(new Get.Query { Url = url }));
        }
    }
}
