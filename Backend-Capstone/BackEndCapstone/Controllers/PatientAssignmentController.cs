using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAssignmentController :ControllerBase
    {
        private readonly IPatientAssignmentRepository _patientAssignmentRepository;
        public PatientAssignmentController(IPatientAssignmentRepository patientAssignmentRepository)
        {
            _patientAssignmentRepository = patientAssignmentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_patientAssignmentRepository.GetPatientAssignments());
        }


        [HttpGet("Provider{id}")]
        public IActionResult GetPatientAssignmentByProviderId(int id)
        {
            var assignments = _patientAssignmentRepository.GetPatientAssignmentByProviderId(id);
            if (assignments == null)
            {
                return NotFound();
            }
            return Ok(assignments);
        }
        [HttpPost]
        public IActionResult Post(PatientAssignment patientAssignment)
        {
            _patientAssignmentRepository.Add(patientAssignment);
            return CreatedAtAction("Get", new { id = patientAssignment.Id }, patientAssignment
                );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PatientAssignment patientAssignment)
        {
            if (id != patientAssignment.Id)
            {
                return BadRequest();
            }

            _patientAssignmentRepository.Update(patientAssignment);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _patientAssignmentRepository.Delete(id);
            return NoContent();
        }


    }
}
