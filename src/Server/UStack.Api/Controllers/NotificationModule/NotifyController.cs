using MediatR;
using Microsoft.AspNetCore.Mvc;
using UStack.Notification.Application.Features.ResetPassword;

namespace UStack.Api.Controllers.NotificationModule;

[ApiController]
[Route("api/notify")]
public class NotifyController : ControllerBase
{
    private readonly ISender _sender;

    public NotifyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("send-token")]
    public async Task<IActionResult> SendResetPasswordToken(ResetPasswordNotificationCommand command)
    {
        try
        {
            await _sender.Send(command);
            return Ok("Notification sent successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
