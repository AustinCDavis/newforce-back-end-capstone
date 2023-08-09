using BackEndCapstone.Repositories;
using BackEndCapstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenAssignmentController : ControllerBase
    {
        private readonly IRegimenAssignmentRepository _regimenAssignmentRepository;

        public RegimenAssignmentController(IRegimenAssignmentRepository regimenAssignmentRepository)
        {
            _regimenAssignmentRepository = regimenAssignmentRepository;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_regimenAssignmentRepository.GetAllregimens());
        //}

        [HttpPost]
        public IActionResult Post(RegimenAssignment regimenAssignment)
        {
            _regimenAssignmentRepository.Add(regimenAssignment);
            return CreatedAtAction("Get", new { id = regimenAssignment.Id }, regimenAssignment
                );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RegimenAssignment regimenAssignment)
        {
            if (id != regimenAssignment.Id)
            {
                return BadRequest();
            }

            _regimenAssignmentRepository.Update(regimenAssignment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _regimenAssignmentRepository.Delete(id);
            return NoContent();
        }



    }
}
