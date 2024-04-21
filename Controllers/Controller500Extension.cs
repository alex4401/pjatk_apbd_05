using Microsoft.AspNetCore.Mvc;

namespace APBD4.Controllers;

public static class Controller500Extension
{
    public static StatusCodeResult InternalServerError(this ControllerBase self)
    {
        return self.StatusCode(StatusCodes.Status500InternalServerError);
    }
}