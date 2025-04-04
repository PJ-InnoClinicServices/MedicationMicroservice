﻿using DrugsMicroservice.Application.DTOs.Substances;
using MedicationMicroservice.Application.Models;

namespace MedicationMicroservice.BusinessLogic.IServices
{
    public interface ISubstancesService
    {
        Task<IEnumerable<Substance>> GetAllSubstancesAsync();  
        Task<Substance> GetSubstanceByIdAsync(Guid id);
        Task<Substance> AddSubstanceAsync(SubstanceCreateDTO substance);  
        Task<Substance> UpdateSubstanceAsync(Guid id, Substance substance); 
        Task<bool> DeleteSubstanceAsync(Guid id);  
        Task<Substance> GetSubstanceByNameAsync(string name);
        Task<IEnumerable<Substance>> GetSubstancesForDiseaseAsync(string diseaseName);
    }
}