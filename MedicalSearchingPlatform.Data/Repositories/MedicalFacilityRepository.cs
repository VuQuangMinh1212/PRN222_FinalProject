﻿using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class MedicalFacilityRepository : IMedicalFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalFacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalFacility>> GetAllAsync()
        {
            return await _context.MedicalFacilities
                .Include(f => f.FacilityServices)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<MedicalFacility> GetByIdAsync(string facilityId)
        {
            return await _context.MedicalFacilities
                .Include(f => f.FacilityServices)
                .FirstOrDefaultAsync(f => f.FacilityId == facilityId);
        }

        public async Task AddAsync(MedicalFacility facility)
        {
            facility.CreatedAt = DateTime.UtcNow;
            facility.ImageUrl = $"/img/testimonials/departments-{new Random().Next(1, 6)}.jpg";

            await _context.MedicalFacilities.AddAsync(facility);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicalFacility facility)
        {
            _context.MedicalFacilities.Update(facility);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string facilityId)
        {
            var facility = await GetByIdAsync(facilityId);
            if (facility != null)
            {
                _context.MedicalFacilities.Remove(facility);
                await _context.SaveChangesAsync();
            }
        }
    }
}
