using InvoiceApp.Models.Dtos;
using InvoiceApp.Models.FormRequests;

namespace InvoiceApp.Services
{
    /// <summary>
    /// Invoice service interface
    /// </summary>
    public interface IInvoiceService
    {

        Task<InvoiceDto> IssueInvoice(InvoiceRequest invoiceRequest);
    }
}
