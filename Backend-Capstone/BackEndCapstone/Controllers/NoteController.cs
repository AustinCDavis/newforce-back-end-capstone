using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet("Provider{id}")]
        public IActionResult GetNotesByProviderId(int id) 
        {
            var notes = _noteRepository.GetNotesByProviderId(id);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        [HttpGet("Patient{id}")]
        public IActionResult GetNotesByPatientId(int id)
        {
            var notes = _noteRepository.GetNotesByPatientId(id);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Post(Note note)
        {
            _noteRepository.Add(note);
            return CreatedAtAction("GetNotesByPatientId", new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }
            _noteRepository.Update(note);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _noteRepository.Delete(id);
            return NoContent();
        }



    }
}
