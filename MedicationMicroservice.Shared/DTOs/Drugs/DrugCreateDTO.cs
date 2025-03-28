﻿namespace MedicationMicroservice.Shared.DTOs.Drugs
{
    public class DrugCreateDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public List<String> Substances { get; set; }
    }
}