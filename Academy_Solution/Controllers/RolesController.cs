using AcademyWeb.Application.Services.RolesService;
using AcademyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AcademyWeb.Controllers
{
    [Authorize(Roles = "Admin")] // 👈 Only Admin can access all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _rolesService.GetAllRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Log.Error(ex,ex.Message);
                return BadRequest(ex.Message);      
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _rolesService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            if (role == null)
                return BadRequest();

            var createdRole = await _rolesService.CreateRoleAsync(role);
            return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRole);
        }
        // DELETE: api/Roles/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _rolesService.DeleteRoleAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
