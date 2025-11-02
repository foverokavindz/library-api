namespace library_api.Domain.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException(string message) : base(message, "CONFLICT", 409)
        {
        }
    }
}