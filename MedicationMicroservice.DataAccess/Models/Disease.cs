
namespace MedicationMicroservice.Application.Models
{


    public class Disease
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Substance> Substances { get; } = [];
    }
}