using AcademyWeb.Application.Services.ApplicationService;
using AcademyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AcademyWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;  
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Application>>> GetAllApplications()
        {
            try
            {
                var applications = await _applicationService.GetAllApplicationAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateApplication([FromBody] Models.Application application)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (application == null)
                    return BadRequest("application data is required");

                var createdApplication = await _applicationService.CreateApplicationAsync(application);
                return CreatedAtAction(nameof(GetUserApplication), new { id = application.Id }, createdApplication);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetUserApplication(Guid id)
        {
            try
            {
                var application = await _applicationService.GetApplicationByIdAsync(id);
                if (application == null)
                    return NotFound($"application with Id {id} not found");

                return Ok(application);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
