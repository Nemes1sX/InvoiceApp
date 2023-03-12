using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;

namespace InvoiceApp.Services
{
    public interface IInvoiceItemService
    {
        List<InvoiceItem> CalculateItemTotalPrice(List<InvoiceItemRequest> invoiceItemRequests, int VATPrecent);
    }
}
