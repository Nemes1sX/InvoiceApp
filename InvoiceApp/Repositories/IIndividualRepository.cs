using InvoiceApp.Models.Entities;

namespace InvoiceApp.Repositories
{
    public interface IIndividualRepository
    {
        Task<Individual> GetIndividualWithCountry(int? individualId);
    }
}
