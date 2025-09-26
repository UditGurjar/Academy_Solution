using AcademyWeb.Application.Services.StudentService;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                Log.Error(ex,ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                    return NotFound($"Student with Id {id} not found");

                return Ok(student);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (student == null)
                    return BadRequest("Student data is required");

                var createdStudent = await _studentService.CreateStudentAsync(student);
                return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(Guid id, [FromBody] Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (student == null || id != student.Id)
                    return BadRequest("Student ID mismatch");

                try
                {
                    var updatedStudent = await _studentService.UpdateStudentAsync(student);
                    return Ok(updatedStudent);
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

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(Guid id)
        {
            try
            {
                if (id == Guid.Empty) return NoContent();
                var result = await _studentService.DeleteStudentAsync(id);
                if (!result)
                    return NotFound($"Student with Id {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
