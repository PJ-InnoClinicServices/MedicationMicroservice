using MedicationMicroservice.Application.Models;

namespace MedicationMicroservice.BusinessLogic.IRepositories
{
    public interface IDiseasesRepository
    {
        Task<IEnumerable<Disease>> GetAllAsync();
        Task<Disease> GetByIdAsync(Guid id);
        Task<Disease> GetDiseaseByNameAsync(string name);
        Task<Disease> AddAsync(Disease disease);
        Task<Disease> UpdateAsync(Disease disease);
        Task<bool> DeleteAsync(Guid id);
    }
}