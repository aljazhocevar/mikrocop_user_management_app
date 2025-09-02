using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserRepositoryService.Data;
using UserRepositoryService.DTOs;
using UserRepositoryService.Models;
using UserRepositoryService.Services;
using Serilog;
using System.Text.Json;

namespace UserRepositoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordService _pw;
        public UsersController(IUserRepository repo, IPasswordService pw) { _repo = repo; _pw = pw; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest req)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = req.UserName,
                FullName = req.FullName,
                Email = req.Email,
                Mobile = req.Mobile,
                Language = req.Language,
                Culture = req.Culture,
                PasswordHash = _pw.HashPassword(req.Password)
            };
            await _repo.CreateAsync(user);
            Log.Information("User created {UserId} by {Client}", user.Id, HttpContext.Items["ClientName"]);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, new UserDto {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Mobile = user.Mobile,
                Language = user.Language,
                Culture = user.Culture
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAll(); // dobimo seznam vseh uporabnikov
            Log.Information("All users retrieved by {Client}", HttpContext.Items["ClientName"]);

            var usersDto = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email,
                Mobile = u.Mobile,
                Language = u.Language,
                Culture = u.Culture
            }).ToList();

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                Log.Warning("Get failed: User {UserId} not found", id);
                return NotFound();
            }
            Log.Information("User retrieved {UserId} by {Client}", u.Id, HttpContext.Items["ClientName"]);
            return Ok(new UserDto {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email,
                Mobile = u.Mobile,
                Language = u.Language,
                Culture = u.Culture
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest req)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                Log.Warning("Update failed: User {UserId} not found", id);
                return NotFound();
            }
            u.UserName = req.UserName ?? u.UserName;
            u.FullName = req.FullName ?? u.FullName;
            u.Email = req.Email ?? u.Email;
            u.Mobile = req.Mobile ?? u.Mobile;
            u.Language = req.Language ?? u.Language;
            u.Culture = req.Culture ?? u.Culture;
            if (!string.IsNullOrEmpty(req.Password)) 
            {
                u.PasswordHash = _pw.HashPassword(req.Password);
            }
            await _repo.UpdateAsync(u);
            
            Log.Information("User updated {UserId} by {Client}", u.Id, HttpContext.Items["ClientName"]);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                Log.Warning("Delete failed: User {UserId} not found", id);
                return NotFound();
            }
            await _repo.DeleteAsync(id);

            Log.Information("User deleted {UserId} by {Client}", u.Id, HttpContext.Items["ClientName"]);
            
            return NoContent();
        }

        [HttpPost("{id}/validate-password")]
        public async Task<IActionResult> ValidatePassword(Guid id, [FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("password", out JsonElement passwordElement))
            {
                Log.Warning("ValidatePassword failed: No password provided for user {UserId}", id);
                return BadRequest(new { error = "password required" });
            }

            string provided = passwordElement.GetString(); // pridobi string iz JsonElement

            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                Log.Warning("ValidatePassword failed: User {UserId} not found", id);
                return NotFound();
            }

            if (string.IsNullOrEmpty(u.PasswordHash))
            {
                Log.Warning("ValidatePassword failed: User {UserId} has no password hash", id);
                return BadRequest(new { error = "User has no password set" });
            }

            var ok = _pw.Verify(u.PasswordHash, provided);

            Log.Information("Password validation for User {UserId} by {Client}: {Result}", u.Id, HttpContext.Items["ClientName"], ok);

            return Ok(new { valid = ok });
        }
    }
}
