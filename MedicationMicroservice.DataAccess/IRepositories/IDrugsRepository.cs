using MedicationMicroservice.Application.Models;

namespace MedicationMicroservice.BusinessLogic.IRepositories
{
    public interface IDrugsRepository
    {
        Task<IEnumerable<Drug>> GetAllDrugsAsync();
        Task<Drug> GetDrugByIdAsync(Guid id);
        Task<Drug> GetDrugByNameAsync(string name);
        Task<IEnumerable<Drug>> GetDrugsByDiseaseAsync(string diseaseName);
        Task<Drug> AddDrugAsync(Drug drug);
        Task<Drug> UpdateDrugAsync(Drug drug);
        Task<bool> DeleteDrugAsync(Guid id);
    }
}