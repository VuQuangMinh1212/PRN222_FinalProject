using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class MedicalServiceRepository : IMedicalServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalService>> GetAllAsync()
        {
            return await _context.MedicalServices.Include(s => s.FacilityServices).ToListAsync();
        }

        public async Task<MedicalService> GetByIdAsync(string serviceId)
        {
            return await _context.MedicalServices
                .Include(s => s.FacilityServices)
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        public async Task AddAsync(MedicalService service)
        {
            await _context.MedicalServices.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicalService service)
        {
            _context.MedicalServices.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string serviceId)
        {
            var service = await GetByIdAsync(serviceId);
            if (service != null)
            {
                _context.MedicalServices.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
