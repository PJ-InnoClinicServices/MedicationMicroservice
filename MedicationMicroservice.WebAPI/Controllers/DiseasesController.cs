﻿
using MedicationMicroservice.Application.Models;
using MedicationMicroservice.BusinessLogic.IServices;
using MedicationMicroservice.Shared.DTOs.Diseases;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDrugsService _drugsService;
        private readonly ISubstancesService _substancesService;
        private readonly IDiseasesService _diseasesService;

        public DiseasesController(IDrugsService drugsService, ISubstancesService substancesService, IDiseasesService diseasesService)
        {
            _drugsService = drugsService;
            _substancesService = substancesService;
            _diseasesService = diseasesService;
        }

        /// <summary>
        /// Gets all diseases.
        /// </summary>
        /// <returns>A list of all diseases.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Disease>), 200)]
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult<IEnumerable<Disease>>> GetAllDiseases()
        {
            var diseases = await _diseasesService.GetAllDiseasesAsync();
            return Ok(diseases);
        }

        /// <summary>
        /// Gets a disease by its ID.
        /// </summary>
        /// <param name="id">The ID of the disease.</param>
        /// <returns>The disease with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Disease), 200)]
        [ProducesResponseType(404)] // Disease not found
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult<Disease>> GetDiseaseById(Guid id)
        {
            var disease = await _diseasesService.GetDiseaseByIdAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            return Ok(disease);
        }

        /// <summary>
        /// Gets a disease by its name.
        /// </summary>
        /// <param name="name">The name of the disease.</param>
        /// <returns>The disease with the specified name.</returns>
        [HttpGet("search/{name}")]
        [ProducesResponseType(typeof(Disease), 200)]
        [ProducesResponseType(404)] // Disease not found
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult<Disease>> GetDiseaseByName(string name)
        {
            var disease = await _diseasesService.GetDiseaseByNameAsync(name);
            if (disease == null)
            {
                return NotFound();
            }
            return Ok(disease);
        }

        /// <summary>
        /// Adds a new disease.
        /// </summary>
        /// <param name="newDiseaseDto">The data transfer object for the new disease.</param>
        /// <returns>The newly created disease.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Disease), 201)]
        [ProducesResponseType(400)] // Bad request in case of invalid data
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult<Disease>> AddDisease([FromBody] DiseaseCreateDTO newDiseaseDto)
        {
            if (newDiseaseDto == null)
            {
                return BadRequest("Disease data is null.");
            }

            var disease = new Disease
            {
                Id = Guid.NewGuid(),
                Name = newDiseaseDto.Name,
            };

            var createdDisease = await _diseasesService.AddDiseaseAsync(disease);

            return CreatedAtAction(nameof(GetDiseaseById), new { id = createdDisease.Id }, createdDisease);
        }

        /// <summary>
        /// Updates an existing disease.
        /// </summary>
        /// <param name="id">The ID of the disease to update.</param>
        /// <param name="diseaseUpdateDto">The data transfer object with updated disease information.</param>
        /// <returns>The updated disease.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Disease), 200)]
        [ProducesResponseType(400)] // Bad request if input data is invalid
        [ProducesResponseType(404)] // Disease not found
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult<Disease>> UpdateDisease(Guid id, [FromBody] DiseaseUpdateDTO diseaseUpdateDto)
        {
            if (diseaseUpdateDto == null)
            {
                return BadRequest("Disease data is null.");
            }

            var updatedDisease = await _diseasesService.UpdateDiseaseAsync(id, new Disease
            {
                Name = diseaseUpdateDto.Name,
            });

            if (updatedDisease == null)
            {
                return NotFound();
            }

            return Ok(updatedDisease);
        }

        /// <summary>
        /// Deletes a disease by its ID.
        /// </summary>
        /// <param name="id">The ID of the disease to delete.</param>
        /// <returns>No content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No content if successfully deleted
        [ProducesResponseType(404)] // Not found if the disease does not exist
        [ProducesResponseType(500)] // Internal server error in case of unexpected issues
        public async Task<ActionResult> DeleteDisease(Guid id)
        {
            var result = await _diseasesService.DeleteDiseaseAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        /// <summary>
        /// Finds drugs for a given disease by its name.
        /// </summary>
        /// <param name="diseaseName">The name of the disease.</param>
        /// <returns>Returns a list of drugs that are used to treat the disease.</returns>
        [HttpGet("{diseaseName}/drugs")]
        [ProducesResponseType(typeof(IEnumerable<Drug>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Drug>>> FindDrugsForDisease(string diseaseName)
        {
            var disease = await _diseasesService.GetDiseaseByNameAsync(diseaseName);
            if (disease == null)
            {
                return NotFound($"Disease '{diseaseName}' not found.");
            }

            var drugs = await _drugsService.GetDrugsForDiseaseAsync(diseaseName);
            return Ok(drugs);
        }

        /// <summary>
        /// Finds substances for a given disease by its name.
        /// </summary>
        /// <param name="diseaseName">The name of the disease.</param>
        /// <returns>Returns a list of substances that are used to treat the disease.</returns>
        [HttpGet("{diseaseName}/substances")]
        [ProducesResponseType(typeof(IEnumerable<Substance>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Substance>>> FindSubstancesForDisease(string diseaseName)
        {
            var disease = await _diseasesService.GetDiseaseByNameAsync(diseaseName);
            if (disease == null)
            {
                return NotFound($"Disease '{diseaseName}' not found.");
            }

            var substances = await _substancesService.GetSubstancesForDiseaseAsync(diseaseName);
            return Ok(substances);
        }
    }
    
    
}
