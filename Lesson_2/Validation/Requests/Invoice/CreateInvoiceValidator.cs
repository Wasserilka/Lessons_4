using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface ICreateInvoiceValidator : IValidationService<CreateInvoiceRequest>
    {

    }

    internal sealed class CreateInvoiceValidator : FluentValidationService<CreateInvoiceRequest>, ICreateInvoiceValidator
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.ContractId)
                .GreaterThan(0)
                .WithErrorCode("BRL-130.1");

            RuleFor(x => x.TaskId)
                .GreaterThan(0)
                .WithErrorCode("BRL-130.2");
        }
    }
}
