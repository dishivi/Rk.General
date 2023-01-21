
using FluentValidation;

namespace Rk.Web.Api.Application.Request.Validator
{
    public class RequestAddEmployeeValidator : AbstractValidator<RequestAddEmployee>
    {
        public RequestAddEmployeeValidator()
        {
            RuleFor(x => x).SetValidator(new RequestAddEmployeePreValidator())
             .DependentRules(() =>
             {
                 // logical validations
             });
        }

        private class RequestAddEmployeePreValidator : AbstractValidator<RequestAddEmployee>
        {
            //  formal validations
            internal RequestAddEmployeePreValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Age).GreaterThan(18);
            }
        }
    }
}
