using System;
using Microsoft.AspNetCore.Mvc;
using TaNoMenu.Exceptions;

namespace TaNoMenu.Controllers
{
    public class Controller : ControllerBase
    {
        protected ActionResult HandleError(Exception exception)
        {
            if (exception is HttpException)
            {
                HttpException httpException = (HttpException) exception;
                switch (httpException.StatusCode)
                {
                    case 422:
                        return ValidationProblem(httpException.Message);
                    case 401:
                        return Unauthorized(httpException.Message); 
                    case 404:
                        return NotFound(httpException.Message);
                    default:
                        return BadRequest(httpException.Message);
                }
            }

            return Problem(exception.Message);
        }
    }
}