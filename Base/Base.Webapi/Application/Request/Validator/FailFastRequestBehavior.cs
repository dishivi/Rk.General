using Core.ExceptionHandler;
using Core.ExceptionHandler.ExceptionHandler;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace Base.Webapi.Application.Request.Validator
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
    {
        /// <summary>
        /// The validators
        /// </summary>
        private readonly IEnumerable<IValidator> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="FailFastRequestBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="next">The next.</param>
        /// <returns></returns>
        /// <exception cref="ATuneCustomException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<object>(request);
            List<ValidationFailure> failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                failures = failures.Select(x => { x.ErrorMessage = Business.GlobalExceptionMessage(x); return x; }).ToList();

                List<ValidationFailure> failures2 = failures.GroupBy(item => new { item.ErrorMessage }).Select(group => group.First()).ToList();

                throw new CustomExceptionHandler(failures2)
                {
                    HttpStatusCode = HttpStatusCode.PreconditionFailed
                };
            }

            TResponse response = await next();
            return response;
        }
    }
}
