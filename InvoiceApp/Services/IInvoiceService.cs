using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;

namespace InvoiceApp.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> IssueInvoice(InvoiceRequest invoiceRequest);
    }
}
