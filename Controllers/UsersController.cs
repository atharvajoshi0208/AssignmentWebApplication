#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentWebApplication;
using AssignmentWebApplication.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace AssignmentWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var products = _context.User
              .Include(p => p.Messages)
              .ToListAsync();
            return await products;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            try
            {

                bool userAlreadyExists = _context.User.Any(x => x.UserName == user.UserName);
                

                if (ModelState.IsValid)
                {
                    if (!userAlreadyExists)
                    {
                        user.DateLastSeen = DateTime.Now;
                       

                        if ( user.Date is not null && user.Date != string.Empty)
                        {
                            user.DateCreated = Convert.ToDateTime(user.Date);
                        }
                        else
                        {
                            user.DateCreated = DateTime.Now;
                        }

                        if (user.Id is not 0)
                        {
                            user.Id = user.user;
                        }
                        _context.User.Add(user);
                        
                        user.Messages.Add(new Messages()
                        {
                            Payload = user.PayLoad
                        });
                        //_context.Messages.Add(tempMessages);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        var modifyingUser = _context.User.FirstOrDefault(x => x.UserName == user.UserName);

                        var modifyingUserId = modifyingUser.Id;
                        modifyingUser.DateLastSeen = DateTime.Now;
                        Messages tempMessages = new Messages()
                        {
                            Payload = user.PayLoad,
                            UserId = modifyingUserId
                        };


                        _context.User.Update(modifyingUser);
                        _context.Messages.Add(tempMessages);

                        try
                        {
                            await _context.SaveChangesAsync();
                            return Ok();

                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!UserExists(modifyingUserId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
