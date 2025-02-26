using System.ComponentModel.DataAnnotations;

namespace DrugsMicroservice.Application.DTOs.Substances;

public class SubstanceCreateDTO
{
    public string Name { get; set; }
    public string Dosage { get; set; }
    
    public List<string> Diseases { get; set; } 
    
    }