using InvoiceApp.DataContext;
using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceDataContext _context;
        private readonly IInvoiceItemService _invoiceItemService;

        public InvoiceService(InvoiceDataContext context, IInvoiceItemService invoiceItemService)
        {
            _context = context;
            _invoiceItemService = invoiceItemService;
        }

        public async Task<Invoice> IssueInvoice(InvoiceRequest invoiceRequest)
        {
            var invoice = new Invoice();
            invoice.BilledLegalPersonId = invoiceRequest.BilledByLegalPersonId;
            var billedLegalPerson = await _context.LegalPersons.Where(x => x.Id == invoice.BilledLegalPersonId).Include(x => x.Country).SingleOrDefaultAsync();
            if (invoiceRequest.PayedByLegalPersonId != null && invoiceRequest.PayedByLegalPersonId > 0)
            {
                invoice.PayedLegalPersonId = invoiceRequest.PayedByLegalPersonId;
                await IssueInvoicePayedLegalPerson(invoice, invoiceRequest.InvoiceItems, billedLegalPerson);
            }
            else if (invoiceRequest.PayedByIndividualId != null && invoiceRequest.PayedByIndividualId > 0)
            {
                invoice.PayedIndividualId = invoiceRequest.PayedByIndividualId;
                await IssueInvoicePayedIndividual(invoice, invoiceRequest.InvoiceItems, billedLegalPerson);
            }
            else
            {
                return null;
            }

            invoice.TotalPrice = invoice.InvoiceItems.Sum(x => x.PriceWithVAT);

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        private async Task IssueInvoicePayedLegalPerson(Invoice invoice, List<InvoiceItemRequest> invoiceItemRequests, LegalPerson billedLegalPerson)
        {
            var payedLegalPerson = await _context.LegalPersons.Where(x => x.Id == invoice.PayedLegalPersonId).Include(x => x.Country).SingleOrDefaultAsync();
            if (payedLegalPerson.Country.EuropeanUnion && billedLegalPerson.VATPayer)
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, billedLegalPerson.Country.VATPrecent);
            }
            else
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, 0);
            }
        }


        private async Task IssueInvoicePayedIndividual(Invoice invoice, List<InvoiceItemRequest> invoiceItemRequests, LegalPerson billedLegalPerson)
        {
            var payedIndividual = await _context.Individuals.Where(x => x.Id == invoice.PayedIndividualId).Include(x => x.Country).SingleOrDefaultAsync();
            if (payedIndividual.Country.EuropeanUnion && billedLegalPerson.VATPayer)
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, billedLegalPerson.Country.VATPrecent);
            }
            else
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, 0);
            }
        }
    }
}
