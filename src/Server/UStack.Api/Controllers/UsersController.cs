using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UStack.Api.Contracts;
using UStack.Identity.Application.Abstraction.Pagination;
using UStack.Identity.Application.UseCases.AdminActions.AssignTeacherToCourse;
using UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;
using UStack.Identity.Application.UseCases.Queries.Users.GetUserById;
using UStack.Identity.Application.UseCases.Queries.Users.GetUsersWithRoles;
using UStack.Identity.Application.UseCases.Users.ActivateUser;
using UStack.Identity.Application.UseCases.Users.AssignRole;
using UStack.Identity.Application.UseCases.Users.CreateUser;
using UStack.Identity.Application.UseCases.Users.DeactivateUser;
using UStack.Identity.Application.UseCases.Users.LoginUser;
using UStack.Identity.Application.UseCases.Users.RemoveRole;
using UStack.Identity.Application.UseCases.Users.ResetPassword;

namespace UStack.Api.Controllers;

[ApiController]
[Route("api/identity/users")]
[SwaggerTag("Identity bilan ishlash")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Yangi User yaratish uchun")]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { userId = result.Value.UserId }, result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost("{userId:guid}/activate")]
    [SwaggerOperation(
        Summary = "Mavjud userni holatini activate qilish")]
    public async Task<IActionResult> Activate(Guid userId)
    {
        var result = await _sender.Send(new ActivateUserCommand(userId));
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("{userId:guid}/deactivate")]
    [SwaggerOperation(
        Summary = "Mavjud userni holatini deactivate qilish")]
    public async Task<IActionResult> Deactivate(Guid userId)
    {
        var result = await _sender.Send(new DeactivateUserCommand(userId));
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "User login qilish uchun endpoint")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("teachers")]
    [SwaggerOperation(
        Summary = "Tizimga yangi teacher rolidagi user qo'shish")]
    public async Task<IActionResult> CreateTeacher(CreateTeacherCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("reset-password")]
    [SwaggerOperation(
        Summary = "Tizimdagi mavjud user parolini update qilsih uchun maxfiy code yuborish (email orqali)")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess 
            ? Ok(new { message = "Code sent to your email address. Check your email inbox "}) 
            : BadRequest(result.Error);
    }

    [HttpPost("confirm-reset-password-token")]
    [SwaggerOperation(
        Summary = "Parol update qilish uchun yuborilgan maxfiy code'ni tasdiqlash")]
    public async Task<IActionResult> ConfirmResetPasswordToken([FromBody] ConfirmResetTokenCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("confirm-reset-password")]
    [SwaggerOperation(
        Summary = "Parol update qilish uchun tasdiqlangan maxfiy code'dan keyin parolni update qilish")]
    public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }


    [HttpPost("{userId:guid}/roles/{roleId:guid}")]
    [SwaggerOperation(
        Summary = "Tizimdagi mavjud user uchun rol biriktirish")]
    public async Task<IActionResult> AssignRole(Guid userId, Guid roleId)
    {
        var result = await _sender.Send(new AssignRoleCommand(userId, roleId));
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
    [SwaggerOperation(
        Summary = "Tizimdagi mavjud user'ning role'ini o'chirish")]
    public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
    {
        var result = await _sender.Send(new RemoveRoleCommand(userId, roleId));
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    // QUERIES

    [HttpGet("{userId:guid}")]
    [SwaggerOperation(
        Summary = "ID bo'yicha tizimdan user qidirish")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var result = await _sender.Send(new GetUserByIdQuery(userId));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Tizimdagi user'lar ro'yxatini olish")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagination = new PaginationParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _sender.Send(new GetUsersWithRolesQuery(pagination));
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{courseId}/assign-teacher/{teacherId}")]
    [SwaggerOperation(
        Summary = "Kursga teacher biriktirish")]
    public async Task<IActionResult> AssignTeacherToCourse(Guid courseId, Guid teacherId)
    {
        var command = new AssignTeacherToCourseCommand(courseId, teacherId);
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

}
