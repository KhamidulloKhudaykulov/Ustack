using MediatR;
using Microsoft.AspNetCore.Mvc;
using UStack.Course.Application.UseCases.Features.AssignTeacher;
using UStack.Course.Application.UseCases.Features.CreateCourse;

namespace UStack.Api.Controllers.CourseModule;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ISender _sender;

    public CoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPatch("{courseId}/assign-teacher/{teacherId}")]
    public async Task<IActionResult> AssignTeacher(Guid courseId, Guid teacherId)
    {
        var command = new AssignTeacherCommand(courseId, teacherId);
        var result = await _sender.Send(command);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}
