using MediatR;
using Microsoft.AspNetCore.Mvc;
using UStack.Users.Application.UseCases.Features.Teachers.CreateTeacher;
using UStack.Users.Application.UseCases.Features.Teachers.DeleteTeacher;
using UStack.Users.Application.UseCases.Queries.Students.GetStudentById;

namespace UStack.Api.Controllers.UsersModule;

[ApiController]
[Route("api/users/teachers")]
public class TeachersController : ControllerBase
{
    private readonly ISender _sender;

    public TeachersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
      [FromBody] CreateTeacherCommand command,
      CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Value },
            response.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new GetStudentByIdQuery(id),
            cancellationToken);

        if (response.IsFailure)
            return NotFound(response.Error);

        return Ok(response.Value);
    }

    [HttpDelete("{identityId:guid}")]
    public async Task<IActionResult> DeleteByIdentityId(
        Guid identityId,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new DeleteTeacherByIdentityIdCommand(identityId),
            cancellationToken);
        if (response.IsFailure)
            return BadRequest(response.Error);
        return NoContent();
    }
}
