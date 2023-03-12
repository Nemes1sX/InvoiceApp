using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;

namespace InvoiceApp.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        public List<InvoiceItem> CalculateItemTotalPrice(List<InvoiceItemRequest> invoiceItemRequests, int VATPrecent)
        {
            var invoiceItems = new List<InvoiceItem>();
            foreach (var invoiceItemRequest in invoiceItemRequests) 
            {
                var invoiceItem = new InvoiceItem();
                invoiceItem.Quantity = invoiceItemRequest.Quantity;
                invoiceItem.BasePrice = Convert.ToInt32(invoiceItemRequest.ItemPrice * 100);
                if (VATPrecent > 0)
                {
                    invoiceItem.PriceWithVAT = invoiceItem.BasePrice * (100 + VATPrecent) / 100;
                }
                else
                {
                    invoiceItem.PriceWithVAT = invoiceItem.BasePrice;
                }
                invoiceItem.TotalItemPrice = invoiceItem.Quantity * invoiceItem.PriceWithVAT;
                invoiceItems.Add(invoiceItem);
            }

            return invoiceItems;
        }
    }
}
