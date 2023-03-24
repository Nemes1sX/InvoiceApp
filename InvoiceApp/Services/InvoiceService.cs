using AutoMapper;
using InvoiceApp.DataContext;
using InvoiceApp.Models.Dtos;
using InvoiceApp.Models.Entities;
using InvoiceApp.Models.FormRequests;
using InvoiceApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IIndividualRepository _individualRepository;
        private readonly ILegalPersonRepository _legalPersonRepository;
        private readonly IInvoiceItemService _invoiceItemService;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IInvoiceItemService invoiceItemService, IIndividualRepository individualRepository, ILegalPersonRepository legalPersonRepository, IMapper mapper)
        {
            _invoiceRepository= invoiceRepository;
            _invoiceItemService = invoiceItemService;
            _individualRepository = individualRepository;
            _legalPersonRepository = legalPersonRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceRequest"></param>
        /// <returns></returns>
        public async Task<InvoiceDto> IssueInvoice(InvoiceRequest invoiceRequest)
        {
            var invoice = new Invoice();
            invoice.BilledLegalPersonId = invoiceRequest.BilledByLegalPersonId;
            var billedLegalPerson = await _legalPersonRepository.GetLegalPersonWithCountry(invoiceRequest.BilledByLegalPersonId);
            if (invoiceRequest.PayedByLegalPersonId != null && invoiceRequest.PayedByLegalPersonId > 0)
            {
                invoice.PayedLegalPersonId = invoiceRequest.PayedByLegalPersonId;
                await CalculateInvoiceItemsPayedByLegalPerson(invoice, invoiceRequest.InvoiceItems, billedLegalPerson);
            }
            else if (invoiceRequest.PayedByIndividualId != null && invoiceRequest.PayedByIndividualId > 0)
            {
                invoice.PayedIndividualId = invoiceRequest.PayedByIndividualId;
                await CalculateInvoiceItemsPayedByIndividual(invoice, invoiceRequest.InvoiceItems, billedLegalPerson);
            }
            else
            {
                return null;
            }

            invoice.TotalPrice = invoice.InvoiceItems.Sum(x => x.PriceWithVAT);
            
            await _invoiceRepository.AddInvoice(invoice);

            return _mapper.Map<InvoiceDto>(invoice);
        }


        private async Task CalculateInvoiceItemsPayedByLegalPerson(Invoice invoice, List<InvoiceItemRequest> invoiceItemRequests, LegalPerson billedLegalPerson)
        {
            var payedLegalPerson = await _legalPersonRepository.GetLegalPersonWithCountry(invoice.PayedLegalPersonId);
            if (payedLegalPerson.Country.EuropeanUnion && billedLegalPerson.VATPayer)
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, billedLegalPerson.Country.VATPrecent);
            }
            else
            {
                invoice.InvoiceItems = _invoiceItemService.CalculateItemTotalPrice(invoiceItemRequests, 0);
            }
        }


        private async Task CalculateInvoiceItemsPayedByIndividual(Invoice invoice, List<InvoiceItemRequest> invoiceItemRequests, LegalPerson billedLegalPerson)
        {
            var payedIndividual = await _individualRepository.GetIndividualWithCountry(invoice.PayedIndividualId);
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
