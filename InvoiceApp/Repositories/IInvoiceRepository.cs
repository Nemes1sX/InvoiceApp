using InvoiceApp.Models.Entities;

namespace InvoiceApp.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddInvoice(Invoice invoice);
    }
}
