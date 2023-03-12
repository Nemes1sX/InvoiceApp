using InvoiceApp.DataContext;
using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;

namespace InvoiceApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceDataContext _context;

        public InvoiceService(InvoiceDataContext context) 
        {
            _context = context;    
        }

        public async Task<Invoice> IssueInvoice(InvoiceRequest invoiceRequest)
        {
            var invoice = new Invoice();
            var invoiceItemService = new InvoiceItemService();
            invoice.BilledLegalPersonId = invoiceRequest.BilledByLegalPersonId;
            var billedLegalPerson = await _context.LegalPersons.FindAsync(invoice.BilledLegalPersonId);
            if (invoiceRequest.PayedByLegalPersonId != null)
            {
                invoice.PayedLegalPersonId = invoiceRequest.PayedByLegalPersonId;
                IssueInvoicePayedLegalPerson(invoice);
            }
            else if (invoiceRequest.PayedByIndividualId != null) 
            {
                invoice.PayedIndividualId = invoiceRequest.PayedByIndividualId;
            }
            else
            {
                return null;
            }

        
            var payedIndividual = await _context.Individuals.FindAsync(invoice.PayedIndividualId);
            if ((payedIndividual.Country.EuropeanUnion && payedIndividual != null) || (payedLegalPerson.Country.EuropeanUnion && payedLegalPerson != null))
            {
                invoice.InvoiceItems = invoiceItemService.CalculateItemTotalPrice(invoiceRequest.InvoiceItems, billedLegalPerson.Country.VATPrecent);
            } else
            {
                invoice.InvoiceItems = invoiceItemService.CalculateItemTotalPrice(invoiceRequest.InvoiceItems, 0);
            }

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        private Invoice IssueInvoicePayedLegalPerson(Invoice invoice)
        {
            var payedLegalPerson = await _context.LegalPersons.FindAsync(invoice.PayedLegalPersonId);
            if (payedLegalPerson.Country.EuropeanUnion)
            {
                invoice.InvoiceItems = invoiceItemService.CalculateItemTotalPrice(invoiceRequest.InvoiceItems, billedLegalPerson.Country.VATPrecent);
            }
            else
            {
                invoice.InvoiceItems = invoiceItemService.CalculateItemTotalPrice(invoiceRequest.InvoiceItems, 0);
            }
        }
    }
}
