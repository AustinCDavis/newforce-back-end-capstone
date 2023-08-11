using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;


        public ExerciseController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_exerciseRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var exercise = _exerciseRepository.GetById(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }


        [HttpGet("Regimen{id}")]
        public IActionResult GetExercisesByRegimenId(int id)
        {
            var exercises = _exerciseRepository.GetExercisesByRegimenId(id);
            if (exercises == null)
            {
                return NotFound();
            }
            return Ok(exercises);
        }

        [HttpGet("Patient{id}")]
        public IActionResult GetExercisesByPatientId(int id)
        {
            var exercises = _exerciseRepository.GetExercisesByPatientId(id);
            if (exercises == null)
            {
                return NotFound();
            }
            return Ok(exercises);
        }

        [HttpPost]
        public IActionResult Post(Exercise exercise)
        {
            _exerciseRepository.Add(exercise);
            return CreatedAtAction("GetExercisesByPatientId", new { id = exercise.Id }, exercise);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }
            _exerciseRepository.Update(exercise);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _exerciseRepository.Delete(id);
            return NoContent();
        }
    }
}
