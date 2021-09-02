using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface ICreateTaskValidator : IValidationService<CreateTaskRequest>
    {

    }

    internal sealed class CreateTaskValidator : FluentValidationService<CreateTaskRequest>, ICreateTaskValidator
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.PricePerHour)
                .GreaterThan(0)
                .WithErrorCode("BRL-140.1");
        }
    }
}
