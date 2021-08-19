using System.Collections.Generic;
using Lesson_2.Models;

namespace Lesson_2.Repositories
{
    public interface IInvoiceRepository
    {
        void CreateInvoice(long _invoiceId);
        void DeleteInvoice(long _invoiceId);
        void PutContractToInvoice(long _invoiceId, long _contractId);
        List<Invoice> GetAllInvoices();
    }
    public class InvoiceRepository : IInvoiceRepository
    {
        List<Invoice> invoices;

        public InvoiceRepository()
        {
            invoices = new List<Invoice>();
        }

        public void CreateInvoice(long _invoiceId)
        {
            foreach (Invoice item in invoices)
            {
                if (item.invoiceId == _invoiceId)
                {
                    return;
                }
            }

            invoices.Add(new Invoice { invoiceId = _invoiceId });
        }

        public void DeleteInvoice(long _invoiceId)
        {
            foreach (Invoice item in invoices)
            {
                if (item.invoiceId == _invoiceId)
                {
                    invoices.Remove(item);
                    break;
                }
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            return invoices;
        }

        public void PutContractToInvoice(long _invoiceId, long _contractId)
        {
            foreach (Invoice item in invoices)
            {
                if (item.invoiceId == _invoiceId)
                {
                    item.contract = _contractId;
                    break;
                }
            }
        }
    }
}
