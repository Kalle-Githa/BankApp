using BankApp.Data.Entities;

namespace BankApp.Data.Interfaces
{
    public interface IDispositionRepo
    {
        Task AddAsync(Disposition disposition);


    }
}
