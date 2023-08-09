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

        //This was associated to the GetExercisesByRegimenID but am thinking of uising the exercise repo for thet
        //[HttpGet("Regimen{id}")]
        //public IActionResult GetExercisesByRegimenId(int id) 
        //{
        //    var exercises = _regimenExerciseRepository.GetExercisesByRegimenId(id);
        //    if (exercises == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(exercises);
        //}

        [HttpPost]
        public IActionResult Post(RegimenExercise regimenExercise)
        {
            _regimenExerciseRepository.Add(regimenExercise);
            return CreatedAtAction("Get", new { id = regimenExercise.Id }, regimenExercise);
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
