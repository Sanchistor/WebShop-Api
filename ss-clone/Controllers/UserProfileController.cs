using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.UsersProfile;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public UserProfileController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<UserProfileController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.Include(c => c.Profile).ToListAsync());
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.Where(c => c.Id == id).Include(c => c.Profile).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            return user;
        }


        // PUT api/<UserProfileController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(UpdateUserProfileDto req, int id)
        {
            var user = await _context.Users.Where(c => c.Id == id).Include(c => c.Profile).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.Email = req.Email;
            user.Phone = req.Phone;
            user.Password = req.Password;
            user.Updated = DateTime.UtcNow;

            if (user.Profile == null)
            {
                // If the user does not have a profile, create a new one and link it to the user
                var profile = new Profile
                {
                    NickName = req.NickName
                };

                user.Profile = profile;
            }
            else
            {
                // Update the profile properties
                user.Profile.NickName = req.NickName;
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully");
        }
    }
}
