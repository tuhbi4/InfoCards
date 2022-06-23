using InfoCards.Common.Entities;
using InfoCards.DAL.DAO;
using InfoCards.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InfoCards.Common.Dependencies
{
    public static class DependencyInjection
    {
        private const string JsonPath = @"..\data\cards.json";

        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<InfoCard>>(x => new JsonCardRepository(JsonPath));
        }
    }
}