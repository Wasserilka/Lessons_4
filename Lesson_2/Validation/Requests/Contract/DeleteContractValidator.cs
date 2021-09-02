using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IDeleteContractValidator : IValidationService<DeleteContractRequest>
    {

    }

    internal sealed class DeleteContractValidator : FluentValidationService<DeleteContractRequest>, IDeleteContractValidator
    {
        public DeleteContractValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-101.1");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithErrorCode("BRL-101.2");
        }
    }
}
