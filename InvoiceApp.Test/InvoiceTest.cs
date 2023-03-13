using AutoMapper;
using InvoiceApp.DataContext;
using InvoiceApp.Infrastructure;
using InvoiceApp.Models.FormRequests;
using InvoiceApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceApp.Test
{
    public class InvoiceTest
    {
        private DbContextOptions<InvoiceDataContext> dbContext = new DbContextOptionsBuilder<InvoiceDataContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;
        private InvoiceService _service;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var context = new InvoiceDataContext(dbContext);
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            new InvoiceSeeding(httpClientFactory, context).Seed();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _service = new InvoiceService(context, new InvoiceItemService(), _mapper);
        }

        [Test]
        public async Task InvoiceService_IssueInvoice_BilledLegalPersonNonVATPayer()
        {
            //Act
            var invoiceRequest = new InvoiceRequest();
            var invoiceItemRequest = new List<InvoiceItemRequest> 
            {
                new InvoiceItemRequest { Name = "Testas", ItemPrice = 1, Quantity = 1 }
            };
            invoiceRequest.BilledByLegalPersonId = 2;
            invoiceRequest.PayedByIndividualId = 1;
            invoiceRequest.InvoiceItems = invoiceItemRequest;

            //Arrange
            var invoice = await _service.IssueInvoice(invoiceRequest);

            //Assert
            Assert.AreEqual(1, invoice.TotalPrice);
        }

        [Test]
        public async Task InvoiceService_IssueInvoice_PayedIndividualOrLegalPersonNonEU()
        {
            //Act
            var invoiceRequest = new InvoiceRequest();
            var invoiceItemRequest = new List<InvoiceItemRequest>
            {
                new InvoiceItemRequest { Name = "Testas", ItemPrice = 1, Quantity = 1 }
            };
            invoiceRequest.BilledByLegalPersonId = 1;
            invoiceRequest.PayedByIndividualId = 1;
            invoiceRequest.InvoiceItems = invoiceItemRequest;

            //Arrange
            var invoice = await _service.IssueInvoice(invoiceRequest);

            //Assert
            Assert.AreEqual(1, invoice.TotalPrice);
        }

        [Test]
        public async Task InvoiceService_IssueInvoice_PayedIndividualOrLegalPersonAndBilledLegalPersonFromEUButFromDifferentCountries() //19 precent VAT because billed legal person is from Cyprus in this case  
        {
            //Act
            var invoiceRequest = new InvoiceRequest();
            var invoiceItemRequest = new List<InvoiceItemRequest>
            {
                new InvoiceItemRequest { Name = "Testas", ItemPrice = 1, Quantity = 1 }
            };
            invoiceRequest.BilledByLegalPersonId = 3;
            invoiceRequest.PayedByIndividualId = 3;
            invoiceRequest.InvoiceItems = invoiceItemRequest;

            //Arrange
            var invoice = await _service.IssueInvoice(invoiceRequest);

            //Assert
            Assert.AreEqual(1.19, invoice.TotalPrice);
        }

        [Test]
        public async Task InvoiceService_IssueInvoice_PayedIndividualOrLegalPersonAndBilledLegalPersonFromEUButFromSameCountries()  //21 precent VAT because billed legal person is from Lithuania in this case  
        {
            //Act
            var invoiceRequest = new InvoiceRequest();
            var invoiceItemRequest = new List<InvoiceItemRequest>
            {
                new InvoiceItemRequest { Name = "Testas", ItemPrice = 1, Quantity = 1 }
            };
            invoiceRequest.BilledByLegalPersonId = 1;
            invoiceRequest.PayedByIndividualId = 2;
            invoiceRequest.InvoiceItems = invoiceItemRequest;

            //Arrange
            var invoice = await _service.IssueInvoice(invoiceRequest);

            //Assert
            Assert.AreEqual(1.21, invoice.TotalPrice);
        }
    }
}