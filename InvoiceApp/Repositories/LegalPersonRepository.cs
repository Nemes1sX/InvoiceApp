using InvoiceApp.DataContext;
using InvoiceApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Repositories
{
    public class LegalPersonRepository : ILegalPersonRepository
    {
        private readonly InvoiceDataContext _context;

        public LegalPersonRepository(InvoiceDataContext context)
        {
            _context = context;
        }

        public async Task<LegalPerson> GetLegalPersonWithCountry(int? legalPersonId)
        {
            return await _context.LegalPersons.Where(x => x.Id == legalPersonId).Include(x => x.Country).SingleOrDefaultAsync();
        }
    }
}
