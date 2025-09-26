using AcademyWeb.Infrastructure.Repositories.GenericRepository;
using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IGenericRepository<Models.Application> _applicationRepository;
        public ApplicationService(IGenericRepository<Models.Application> applicationRepository)
        {
                _applicationRepository= applicationRepository;
        }

        public async Task<Models.Application> CreateApplicationAsync(Models.Application application)
        {
            try
            {
                if (application == null)
                    throw new ArgumentNullException(nameof(application));

                return await _applicationRepository.AddAsync(application);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteApplicationAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid Application Id", nameof(id));

                return await _applicationRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;

            }
        }

        public async Task<IEnumerable<Models.Application>> GetAllApplicationAsync()
        {
            try
            {
                return await _applicationRepository.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Models.Application?> GetApplicationByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid Application Id", nameof(id));

                return await _applicationRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Models.Application> UpdateApplicationAsync(Models.Application application)
        {
            try
            {

                if (application == null)
                    throw new ArgumentNullException(nameof(application));

                var existingApplication = await _applicationRepository.GetByIdAsync(application.Id);
                if (existingApplication == null)
                    throw new KeyNotFoundException($"Course with Id {application.Id} not found");

                // Update fields
                existingApplication.CourseOfferingId = application.CourseOfferingId;
                existingApplication.Status = application.Status;

                return await _applicationRepository.UpdateAsync(existingApplication);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
