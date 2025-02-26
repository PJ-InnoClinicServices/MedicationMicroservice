using DrugsMicroservice.Application.DTOs;
using MedicationMicroservice.Application.Models;
using MedicationMicroservice.BusinessLogic.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugsService _drugsService;
        private readonly ISubstancesService _substancesService;

        public DrugsController(IDrugsService drugsService, ISubstancesService substancesService)
        {
            _drugsService = drugsService;
            _substancesService = substancesService;
        }

        /// <summary>
        /// Retrieves all drugs.
        /// </summary>
        /// <returns>A list of all drugs.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Drug>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Drug>>> GetAllDrugs()
        {
            var drugs = await _drugsService.GetAllDrugsAsync();
            return Ok(drugs);
        }

        /// <summary>
        /// Retrieves a drug by its ID.
        /// </summary>
        /// <param name="id">The ID of the drug to retrieve.</param>
        /// <returns>The drug with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Drug), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drug>> GetDrugById(Guid id)
        {
            var drug = await _drugsService.GetDrugByIdAsync(id);
            if (drug == null)
            {
                return NotFound();
            }

            return Ok(drug);
        }

        /// <summary>
        /// Searches for a drug by its name.
        /// </summary>
        /// <param name="name">The name of the drug to search for.</param>
        /// <returns>The drug with the specified name.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(Drug), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drug>> GetDrugByName([FromQuery] string name)
        {
            var drug = await _drugsService.GetDrugByNameAsync(name);
            if (drug == null)
            {
                return NotFound($"Drug '{name}' not found.");
            }

            return Ok(drug);
        }

        /// <summary>
        /// Creates a new drug.
        /// </summary>
        /// <param name="newDrugDto">The details of the drug to create.</param>
        /// <returns>The created drug.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Drug), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drug>> CreateDrug([FromBody] DrugCreateDTO newDrugDto)
        {
            if (newDrugDto == null)
            {
                return BadRequest("Drug data is null.");
            }

            var createdDrug = await _drugsService.AddDrugAsync(newDrugDto);
            if (createdDrug == null)
            {
                return BadRequest("There was an error while creating the drug.");
            }

            return CreatedAtAction(nameof(GetDrugById), new { id = createdDrug.Id }, createdDrug);
        }

        /// <summary>
        /// Updates an existing drug.
        /// </summary>
        /// <param name="id">The ID of the drug to update.</param>
        /// <param name="drugUpdateDto">The updated details of the drug.</param>
        /// <returns>The updated drug.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Drug), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drug>> UpdateDrug(Guid id, [FromBody] DrugUpdateDTO drugUpdateDto)
        {
            if (drugUpdateDto == null)
            {
                return BadRequest("Drug data is null.");
            }

            var updatedDrug = await _drugsService.UpdateDrugAsync(id, new Drug
            {
                Name = drugUpdateDto.Name,
                Manufacturer = drugUpdateDto.Manufacturer,
                Price = drugUpdateDto.Price
            });

            if (updatedDrug == null)
            {
                return NotFound();
            }

            return Ok(updatedDrug);
        }

        /// <summary>
        /// Deletes a drug.
        /// </summary>
        /// <param name="id">The ID of the drug to delete.</param>
        /// <returns>A status indicating whether the drug was deleted.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteDrug(Guid id)
        {
            var result = await _drugsService.DeleteDrugAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
