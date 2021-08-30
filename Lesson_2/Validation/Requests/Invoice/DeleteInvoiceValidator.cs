using FluentValidation;
using Timesheets.Requests;

namespace Timesheets.Validation.Requests
{
    public interface IDeleteInvoiceValidator : IValidationService<DeleteInvoiceRequest>
    {

    }

    internal sealed class DeleteInvoiceValidator : FluentValidationService<DeleteInvoiceRequest>, IDeleteInvoiceValidator
    {
        public DeleteInvoiceValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode("BRL-131.1");
        }
    }
}
