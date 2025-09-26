using AcademyWeb.Infrastructure.Repositories.GenericRepository;
using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseService(IGenericRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Course> CreateCourseAsync(Course course)
        {
            try
            {
                if (course == null)
                    throw new ArgumentNullException(nameof(course));

                return await _courseRepository.AddAsync(course);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid course Id", nameof(id));

                return await _courseRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;

            }
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                return await _courseRepository.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid course Id", nameof(id));

                return await _courseRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            try
            {

                if (course == null)
                    throw new ArgumentNullException(nameof(course));

                var existingCourse = await _courseRepository.GetByIdAsync(course.Id);
                if (existingCourse == null)
                    throw new KeyNotFoundException($"Course with Id {course.Id} not found");

                // Update fields
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Accreditation = course.Accreditation;
                existingCourse.SAQAId = course.SAQAId;
                existingCourse.Level = course.Level;
                existingCourse.NumberOfCredits = course.NumberOfCredits;
                existingCourse.IsActive = existingCourse.IsActive;

                return await _courseRepository.UpdateAsync(existingCourse);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
