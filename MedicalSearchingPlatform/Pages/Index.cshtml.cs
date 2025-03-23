using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<MedicalFacility> LatestFacilities { get; set; } = new List<MedicalFacility>();
    public List<Doctor> LatestDoctors { get; set; } = new List<Doctor>();

    public async Task OnGetAsync()
    {
        if (_context.MedicalFacilities != null)
        {
            LatestFacilities = await _context.MedicalFacilities
                .OrderByDescending(f => f.CreatedAt)
                .Take(3)
                .ToListAsync();
        }

        if (_context.Doctors != null)
        {
            LatestDoctors = await _context.Doctors
                .Include(d => d.User) // Ensure Doctor Name is retrieved from User table
                .OrderByDescending(d => d.CreatedAt)
                .Take(3)
                .ToListAsync();
        }
    }
}
