using library_api.Application.Exceptions;
using library_api.Application.Models;
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
            throw new NotFoundException("TestResource", "test-id");
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
            throw new ValidationException(errors);
        }

        /// <summary>
        /// Test BusinessRuleViolationException
        /// </summary>
        [HttpGet("business-rule")]
        public ActionResult<ApiResponse<object>> TestBusinessRuleException()
        {
            throw new BusinessRuleViolationException("Cannot perform this operation during business hours");
        }

        /// <summary>
        /// Test ConflictException
        /// </summary>
        [HttpGet("conflict")]
        public ActionResult<ApiResponse<object>> TestConflictException()
        {
            throw new ConflictException("Resource already exists with the same identifier");
        }

        /// <summary>
        /// Test ForbiddenException
        /// </summary>
        [HttpGet("forbidden")]
        public ActionResult<ApiResponse<object>> TestForbiddenException()
        {
            throw new ForbiddenException("You don't have permission to access this resource");
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
