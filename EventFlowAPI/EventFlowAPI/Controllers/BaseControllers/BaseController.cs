using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventFlowAPI.Controllers.BaseControllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult HandleErrorResponse<TEntity>(Result<TEntity> result)
        {
            return result.Error.Details!.Code switch
            {
                HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                HttpStatusCode.NotFound => NotFound(result.Error.Details),
                HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, result.Error.Details),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
            };
        } 
    }
}
