using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface ICloseTaskValidator : IValidationService<CloseTaskRequest>
    {

    }

    internal sealed class CloseTaskValidator : FluentValidationService<CloseTaskRequest>, ICloseTaskValidator
    {
        public CloseTaskValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-145.1");
        }
    }
}
