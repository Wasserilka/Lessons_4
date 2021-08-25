using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IAddEmployeeToTaskValidator : IValidationService<AddEmployeeToTaskRequest>
    {

    }

    internal sealed class AddEmployeeToTaskValidator : FluentValidationService<AddEmployeeToTaskRequest>, IAddEmployeeToTaskValidator
    {
        public AddEmployeeToTaskValidator()
        {
            RuleFor(x => x.TaskId)
                .GreaterThan(0)
                .WithErrorCode("BRL-143.1");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithErrorCode("BRL-143.2");
        }
    }
}
