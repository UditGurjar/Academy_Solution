using AcademyWeb.Application.Services.CourseService;
using AcademyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace AcademyWeb.Controllers
{
    [Authorize(Roles = "Admin")] // 👈 Only Admin can access all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                var allCourses = await _courseService.GetAllCoursesAsync();
                return Ok(allCourses);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(Guid id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course == null)
                    return NotFound($"Student with Id {id} not found");

                return Ok(course);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (course == null)
                    return BadRequest("Student data is required");
                var result=await _courseService.CreateCourseAsync(course);
                return Ok(result);

            }
            catch (Exception ex)
            {

                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(Guid id, [FromBody] Course course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (course == null || id != course.Id)
                    return BadRequest("Course ID mismatch");

                try
                {
                    var updatedCourse = await _courseService.UpdateCourseAsync(course);
                    return Ok(updatedCourse);
                }
                catch (KeyNotFoundException ex)
                {
                    Log.Error(ex, ex.Message);
                    return NotFound(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


    }
}
