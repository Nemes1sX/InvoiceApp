using InvoiceApp.DataContext;
using InvoiceApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Repositories
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly InvoiceDataContext _context;

        public IndividualRepository(InvoiceDataContext context)
        {
            _context = context;
        }

        public async Task<Individual> GetIndividualWithCountry(int? individualId)
        {
            return await _context.Individuals.Where(x => x.Id == individualId).Include(x => x.Country).SingleOrDefaultAsync();
        }
    }
}
