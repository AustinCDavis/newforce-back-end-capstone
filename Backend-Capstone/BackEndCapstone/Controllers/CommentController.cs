using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("Exercise{id}")]
        public IActionResult GetCommentsByExerciseId(int id)
        {
            var comments = _commentRepository.GetCommentsByExerciseId(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }


        [HttpPost]
        public IActionResult Post(Comment comment)
        {
            _commentRepository.Add(comment);
            return CreatedAtAction("GetCommentsByExerciseId", new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            _commentRepository.Update(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return NoContent();
        }



    }
}
