using BackEndCapstone.Models;
using BackEndCapstone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        //private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;
        public UserProfileController(IUserRepository userRepository)
        {
            //_userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var profile = _userRepository.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }



        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            if (email == null || user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(UserProfile userProfile)
        {
            _userRepository.Add(userProfile);
            return CreatedAtAction(
                "GetByEmail",
                new { email = userProfile.Email },
                userProfile);
        }

        // PUT api/UserProfile/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _userRepository.Update(userProfile);
            return NoContent();
        }

        // DELETE api/UserProfile/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return NoContent();
        }
    }
    }
