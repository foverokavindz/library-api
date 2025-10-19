using System.ComponentModel.DataAnnotations;
using library_api.Application.Models;
using library_api.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    /// <summary>
    /// Demo controller to test exception handling
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorTestController : ControllerBase
    {
        /// <summary>
        /// Test NotFoundException
        /// </summary>
        [HttpGet("not-found")]
        public ActionResult<ApiResponse<object>> TestNotFoundException()
        {
            throw new NotFoundException(new Guid());
        }

        /// <summary>
        /// Test ValidationException
        /// </summary>
        [HttpGet("validation-error")]
        public ActionResult<ApiResponse<object>> TestValidationException()
        {
            var errors = new Dictionary<string, string[]>
            {
                {"Name", new[] {"Name is required"}},
                {"Email", new[] {"Email is invalid", "Email is already taken"}}
            };
            throw new ValidationException(errors.ToString());
        }

        /// <summary>
        /// Test generic exception
        /// </summary>
        [HttpGet("generic")]
        public ActionResult<ApiResponse<object>> TestGenericException()
        {
            throw new InvalidOperationException("This is a generic exception for testing");
        }
    }
}
