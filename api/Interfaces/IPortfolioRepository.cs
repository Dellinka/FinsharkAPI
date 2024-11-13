using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolio(AppUser user);
        public Task<Portfolio> CreateAsync(Portfolio portfolioModel);
        public Task<Portfolio> DeleteAsync(AppUser appUser, string symbol);
    }
}