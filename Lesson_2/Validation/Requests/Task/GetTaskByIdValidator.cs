using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IGetTaskByIdValidator : IValidationService<GetTaskByIdRequest>
    {

    }

    internal sealed class GetTaskByIdValidator : FluentValidationService<GetTaskByIdRequest>, IGetTaskByIdValidator
    {
        public GetTaskByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-142.1");
        }
    }
}
