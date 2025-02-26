using MedicationMicroservice.Application.Repositories;
using MedicationMicroservice.BusinessLogic.IRepositories;
using MedicationMicroservice.BusinessLogic.IServices;
using MedicationMicroservice.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedicationMicroservice.BusinessLogic.Extensions
{
    
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDrugsService, DrugsService>();
            services.AddScoped<ISubstancesService, SubstancesService>();
            services.AddScoped<IDiseasesService, DiseasesService>();

            services.AddScoped<IDrugsRepository, DrugsRepository>();
            services.AddScoped<ISubstancesRepository, SubstancesRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
        }
    }
}