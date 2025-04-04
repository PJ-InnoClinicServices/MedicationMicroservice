﻿using DrugsMicroservice.Application.DTOs;
using MedicationMicroservice.Application.Models;

namespace MedicationMicroservice.BusinessLogic.IServices
{
    public interface IDrugsService
    {
        Task<IEnumerable<Drug>> GetAllDrugsAsync();
        Task<Drug> GetDrugByIdAsync(Guid id);
        Task<Drug> GetDrugByNameAsync(string name);
        Task<Drug> AddDrugAsync(DrugCreateDTO drug);
        Task<Drug> UpdateDrugAsync(Guid id, Drug drug);
        Task<bool> DeleteDrugAsync(Guid id);
        Task<IEnumerable<Drug>> GetDrugsForDiseaseAsync(string diseaseName);
    }
    }
