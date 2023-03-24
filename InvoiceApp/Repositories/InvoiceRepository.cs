using InvoiceApp.DataContext;
using InvoiceApp.Models.Entities;

namespace InvoiceApp.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDataContext _context;

        public InvoiceRepository(InvoiceDataContext context)
        {
            _context = context;
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
