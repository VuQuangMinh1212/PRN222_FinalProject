using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class AppoimentMedicalServiceRepository : IAppoimentMedicalServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public AppoimentMedicalServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAppointmentMedicalService(List<AppointmentsServices> data)
        {
            await _context.AppointmentServices.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}
