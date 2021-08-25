using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IDeleteTaskValidator : IValidationService<DeleteTaskRequest>
    {

    }

    internal sealed class DeleteTaskValidator : FluentValidationService<DeleteTaskRequest>, IDeleteTaskValidator
    {
        public DeleteTaskValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-141.1");
        }
    }
}
