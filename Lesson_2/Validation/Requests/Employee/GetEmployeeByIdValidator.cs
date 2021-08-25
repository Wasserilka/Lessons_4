using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetEmployeeByIdValidator : IValidationService<GetEmployeeByIdRequest>
    {

    }

    internal sealed class GetEmployeeByIdValidator : FluentValidationService<GetEmployeeByIdRequest>, IGetEmployeeByIdValidator
    {
        public GetEmployeeByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-122.1");
        }
    }
}
