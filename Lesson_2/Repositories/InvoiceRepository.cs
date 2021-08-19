using System.Collections.Generic;
using Lesson_2.Models;
using Lesson_2.Requests;

namespace Lesson_2.Repositories
{
    public interface IInvoiceRepository
    {
        void CreateInvoice(CreateInvoiceRequest request);
        void DeleteInvoice(DeleteInvoiceRequest request);
        List<Invoice> GetAllInvoices();
        Invoice GetInvoiceById(GetInvoiceByIdRequest request);
    }
    public class InvoiceRepository : IInvoiceRepository
    {
        IDataRepository _data;

        public InvoiceRepository(IDataRepository data)
        {
            _data = data;
        }

        public void CreateInvoice(CreateInvoiceRequest request)
        {
            _data.invoiceCounter++;

            Contract _contract = null;

            foreach (Contract item in _data.contracts)
            {
                if (item.Id == request.ContractId)
                {
                    _contract = item;
                    break;
                }
            }

            if (_contract != null)
            {
                var newInvoice = new Invoice { Id = _data.invoiceCounter, Contract = _contract };

                var hours = (newInvoice.Contract.Job.End.ToUnixTimeSeconds() - newInvoice.Contract.Job.Start.ToUnixTimeSeconds()) / 3600;
                newInvoice.Price = hours * newInvoice.Contract.Job.PricePerHour;

                _data.invoices.Add(newInvoice);
            }
        }

        public void DeleteInvoice(DeleteInvoiceRequest request)
        {
            foreach (Invoice item in _data.invoices)
            {
                if (item.Id == request.Id)
                {
                    _data.invoices.Remove(item);
                    break;
                }
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            return _data.invoices;
        }

        public Invoice GetInvoiceById(GetInvoiceByIdRequest request)
        {
            foreach (Invoice invoice in _data.invoices)
            {
                if (invoice.Id == request.Id)
                {
                    return invoice;
                }
            }

            return null;
        }
    }
}
