using AcademyWeb.Infrastructure.Repositories.GenericRepository;
using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepo;

        public StudentService(IGenericRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo ?? throw new ArgumentNullException(nameof(studentRepo));
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student));

                student.UpdatedAt = DateTime.UtcNow; // set on creation
                return await _studentRepo.AddAsync(student);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid student Id", nameof(id));

                return await _studentRepo.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            try
            {
                return await _studentRepo.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid student Id", nameof(id));

                return await _studentRepo.GetByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student));

                var existingStudent = await _studentRepo.GetByIdAsync(student.Id);
                if (existingStudent == null)
                    throw new KeyNotFoundException($"Student with Id {student.Id} not found");

                // Update fields
                existingStudent.StudentName = student.StudentName;
                existingStudent.ContactNo = student.ContactNo;
                existingStudent.Address = student.Address;
                existingStudent.Email = student.Email;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Gender = student.Gender;
                existingStudent.UpdatedAt = DateTime.UtcNow;

                return await _studentRepo.UpdateAsync(existingStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
