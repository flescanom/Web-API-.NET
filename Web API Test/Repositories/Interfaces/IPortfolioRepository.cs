using Web_API_Test.Models;

namespace Web_API_Test.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {

        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol);
    }
}
