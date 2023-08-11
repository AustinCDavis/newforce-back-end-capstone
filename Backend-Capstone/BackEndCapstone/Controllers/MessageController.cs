using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet("User{id}")]
        public IActionResult GetMessagesByUserId(int id)
        {
            var messages = _messageRepository.GetMessagesByUserId(id);
            if (messages == null)
            {
                return NotFound();
            }
            return Ok(messages);
        }

        [HttpGet("From{fromId}/To{toId}")]
        public IActionResult GetMessagesByFromIdAndToId(int fromId, int toId)
        {
            var messages = _messageRepository.GetMessagesByFromIdAndToId(fromId, toId);
            if (messages == null)
            {
                return NotFound();
            }
            return Ok(messages);
        }

        [HttpPost]
        public IActionResult Post(Message message)
        {
            _messageRepository.Add(message);
            return CreatedAtAction("GetMessagesByUserId", new { id = message.Id }, message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _messageRepository.Delete(id);
            return NoContent();
        }


    }
}
