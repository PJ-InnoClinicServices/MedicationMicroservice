using System.Text.Json.Serialization;

namespace MedicationMicroservice.Application.Models
{


    public class Substance
    {
        public Guid Id { get; set; }
        public string SubstanceName { get; set; }
        public string Dosage { get; set; }

        [JsonIgnore] public List<Drug> Drugs { get; } = [];
        [JsonIgnore] public List<Disease> Diseases { get; } = [];

    }
}