using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenExerciseController : ControllerBase
    {
        private readonly IRegimenExerciseRepository _regimenExerciseRepository;

        public RegimenExerciseController(IRegimenExerciseRepository regimenExerciseRepository)
        {
            _regimenExerciseRepository = regimenExerciseRepository;
        }

        //[HttpGet("Regimen{regimenId}/Exercise{exerciseId}")]
        //public IActionResult GetRegimenExerciseByRegimenIdAndExerciseId(int regimenId, int exerciseId)
        //{
        //    var regimenExercise = _regimenExerciseRepository.GetRegimenExerciseByRegimenIdAndExerciseId(regimenId, exerciseId);
        //    if (regimenExercise == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(regimenExercise);
        //}

        [HttpPost]
        public IActionResult Post(RegimenExercise regimenExercise)
        {
            _regimenExerciseRepository.Add(regimenExercise);
            return CreatedAtAction("Put", new { id = regimenExercise.Id }, regimenExercise);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, RegimenExercise regimenExercise)
        {
            if (id != regimenExercise.Id)
            {
                return BadRequest();
            }
            _regimenExerciseRepository.Update(regimenExercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _regimenExerciseRepository.Delete(id);
            return NoContent();
        }

    }
}
