using InvoiceApp.Models.FormRequests;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [Route("issue")]
        public async Task<IActionResult> IssueInvoice(InvoiceRequest invoiceRequest)
        {
            var invoice = await _invoiceService.IssueInvoice(invoiceRequest);

            if (invoice == null)
            {
                return NotFound(new{ message = "Invoied isn't issued"});
            }

            return Ok(invoice);
        }
    }
}
