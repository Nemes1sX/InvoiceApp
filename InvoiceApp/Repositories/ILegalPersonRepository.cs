using InvoiceApp.Models.Entities;

namespace InvoiceApp.Repositories
{
    public interface ILegalPersonRepository
    {
        Task<LegalPerson> GetLegalPersonWithCountry(int? legalPersonId);
    }
}
