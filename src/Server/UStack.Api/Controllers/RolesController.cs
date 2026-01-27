using MediatR;
using Microsoft.AspNetCore.Mvc;
using UStack.Api.Contracts;
using UStack.Identity.Application.UseCases.Roles.ActivateRole;
using UStack.Identity.Application.UseCases.Roles.CreateRole;
using UStack.Identity.Application.UseCases.Roles.DeactivateRole;
using UStack.Identity.Application.UseCases.Roles.UpdateRoleName;

namespace UStack.Api.Controllers;

[ApiController]
[Route("api/identity/roles")]
public class RolesController : BaseController
{
    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }

    // POST: api/roles
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result) 
            : BadRequest(result.Error);
    }

    // PUT: api/roles/{id}/activate
    [HttpPut("{id:guid}/activate")]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ActivateRoleCommand(id),
            cancellationToken);

        return result.IsSuccess 
            ? Ok(result) 
            : BadRequest(result.Error);
    }

    // PUT: api/roles/{id}/deactivate
    [HttpPut("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeactivateRoleCommand(id),
            cancellationToken);

        return result.IsSuccess 
            ? Ok(result) 
            : BadRequest(result.Error);
    }

    // PUT: api/roles/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateName(
        Guid id,
        [FromBody] UpdateRoleNameRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoleNameCommand(id, request.Name);
        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Error);
    }
}
