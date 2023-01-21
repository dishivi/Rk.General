using FluentValidation.Results;
using System.Net;

namespace Core.ExceptionHandler
{
    public class CustomExceptionHandler : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ErrorCode { get; set; }

        public List<ValidationFailure> FluentExceptions { get; private set; }

        public CustomExceptionHandler() : base() { }

        public CustomExceptionHandler(string message) : base(message) { }

        
        public CustomExceptionHandler(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public CustomExceptionHandler(string message, Exception innerException)
             : base(message, innerException) { }

        public CustomExceptionHandler(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        public CustomExceptionHandler(List<ValidationFailure> myStrings)
        {
            this.FluentExceptions = myStrings;
        }
    }
}