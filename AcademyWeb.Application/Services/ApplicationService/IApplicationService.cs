using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<IEnumerable<AcademyWeb.Models.Application>> GetAllApplicationAsync();
        Task<AcademyWeb.Models.Application?> GetApplicationByIdAsync(Guid id);
        Task<AcademyWeb.Models.Application> CreateApplicationAsync(AcademyWeb.Models.Application application);
        Task<AcademyWeb.Models.Application> UpdateApplicationAsync(AcademyWeb.Models.Application application);
        Task<bool> DeleteApplicationAsync(Guid id);
    }
}
