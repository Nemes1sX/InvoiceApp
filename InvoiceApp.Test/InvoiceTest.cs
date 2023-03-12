using InvoiceApp.DataContext;
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


        [SetUp]
        public void Setup()
        {
            var context = new InvoiceDataContext(dbContext);
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            new InvoiceSeeding(httpClientFactory, context).Seed();
            _service = new InvoiceService(context);
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
        public async Task InvoiceService_IssueInvoice_IndividualNonEU()
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
        public async Task InvoiceService_IssueInvoice_IndividualAndLegalPersonFromEUButFromDifferentCountries()
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
            Assert.AreEqual(1.05, invoice.TotalPrice);
        }

        [Test]
        public async Task InvoiceService_IssueInvoice_IndividualAndLegalPersonFromEUButFromSameCountries()
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