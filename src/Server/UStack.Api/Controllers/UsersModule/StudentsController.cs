using MediatR;
using Microsoft.AspNetCore.Mvc;
using UStack.Users.Application.Abstraction.Pagination;
using UStack.Users.Application.UseCases.Features.Students.CreateStudent;
using UStack.Users.Application.UseCases.Features.Students.UpdateStudent;
using UStack.Users.Application.UseCases.Features.Users;
using UStack.Users.Application.UseCases.Queries.Students.GetStudentById;
using UStack.Users.Application.UseCases.Queries.Students.GetStudents;
using UStack.Users.Application.UseCases.Queries.Students.GetStudentsByState;
using UStack.Users.Domain.Enums;

namespace UStack.Api.Controllers.UsersModule;

[ApiController]
[Route("api/users/students")]
public class StudentsController : ControllerBase
{
    private readonly ISender _sender;

    public StudentsController(ISender sender)
    {
        _sender = sender;
    }

    // ---------------------------
    // CREATE STUDENT
    // POST: api/users/students
    // ---------------------------
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStudentCommand command,
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

    // ---------------------------
    // UPDATE STUDENT PROFILE
    // PUT: api/users/students/{id}
    // ---------------------------
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateStudentProfileCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return NoContent();
    }

    // ---------------------------
    // GET STUDENT BY ID
    // GET: api/users/students/{id}
    // ---------------------------
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

    // ---------------------------
    // GET STUDENTS (PAGINATED)
    // GET: api/users/students?pageNumber=1&pageSize=10
    // ---------------------------
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new GetStudentsQuery(pagination),
            cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return Ok(response.Value);
    }

    // ---------------------------
    // GET STUDENTS BY STATE
    // GET: api/users/students/state/{state}
    // ---------------------------
    [HttpGet("state/{state}")]
    public async Task<IActionResult> GetByState(
        UserState state,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new GetStudentsByStateQuery(state, pagination),
            cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return Ok(response.Value);
    }

    // ---------------------------
    // CHANGE STUDENT STATE
    // PATCH: api/users/students/state
    // ---------------------------
    [HttpPatch("state")]
    public async Task<IActionResult> ChangeState(
        [FromBody] ChangeUserStateCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return NoContent();
    }
}
