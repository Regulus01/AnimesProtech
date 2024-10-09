using System.Net;
using AnimesProtech.Domain.Interface.Notification;
using AnimesProtech.Infra.CrossCutting.Notification.Response;
using Microsoft.AspNetCore.Mvc;

namespace AnimesProtech.Infra.CrossCutting.Controller;

public class BaseController : ControllerBase
{
    private readonly INotify _notify;

    public BaseController(INotify notify)
    {
        _notify = notify;
    }
    
    protected new IActionResult Response(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
    {
        if (!_notify.HasNotifications())
        {
            return GenerateSucessResponseForStatusCode(statusCode, result);
        }

        return GenerateErrorResponseForStatusCode(statusCode);
    }

    private IActionResult GenerateErrorResponseForStatusCode(HttpStatusCode statusCode)
    {
        var notifications = _notify.GetNotifications();

        if (HttpStatusCode.NotFound == statusCode)
        {
            return NotFound(new ErrorResponse(notifications.ToList()));
        }

        return BadRequest(new ErrorResponse(notifications.ToList()));
    }

    private IActionResult GenerateSucessResponseForStatusCode(HttpStatusCode statusCode, object? result = null)
    {
        if (statusCode == HttpStatusCode.Created)
        {
            return Created(string.Empty, new SuccessResponse(result));
        }

        if (HttpStatusCode.NoContent == statusCode)
        {
            return NoContent();
        }

        return Ok(new SuccessResponse(result));
    }
}