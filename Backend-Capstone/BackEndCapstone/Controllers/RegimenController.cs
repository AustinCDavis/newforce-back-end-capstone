using BackEndCapstone.Repositories;
using BackEndCapstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenController : ControllerBase
    {
        private readonly IRegimenRepository _regimenRepository;
        public RegimenController(IRegimenRepository regimenRepository)
        {
            _regimenRepository = regimenRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_regimenRepository.GetAll());
        }

        [HttpGet("Regimen{id}")]
        public IActionResult GetRegimenById(int id)
        {
            var regimen = _regimenRepository.GetRegimenById(id);
            if (regimen == null)
            {
                return NotFound();
            }
            return Ok(regimen);
        }

        [HttpGet("Patient{id}")]
        public IActionResult GetRegimensByPatientId(int id)
        {
            var regimen = _regimenRepository.GetPatientRegimensById(id);
            if (regimen == null)
            {
                return NotFound();
            }
            return Ok(regimen);
        }

        [HttpGet("Provider{id}")]
        public IActionResult GetRegimensByProviderId(int id)
        {
            var regimen = _regimenRepository.GetProviderRegimensById(id);
            if (regimen == null)
            {
                return NotFound();
            }
            return Ok(regimen);
        }

        [HttpPost]
        public IActionResult Post(Regimen regimen) 
        {
            _regimenRepository.Add(regimen);
            return CreatedAtAction("Get", new { id = regimen.Id }, regimen);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Regimen regimen)
        {
            if (id != regimen.Id)
            {
                return BadRequest();
            }
            _regimenRepository.Update(regimen);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _regimenRepository.Delete(id);
            return NoContent();
        }

    }
}
