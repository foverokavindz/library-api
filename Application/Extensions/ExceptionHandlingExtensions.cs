using library_api.Application.Middleware;

namespace library_api.Application.Extensions
{
    /// <summary>
    /// Extension methods for configuring exception handling
    /// </summary>
    public static class ExceptionHandlingExtensions
    {
        /// <summary>
        /// Add exception handling middleware to the application pipeline
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <returns>The application builder</returns>
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
