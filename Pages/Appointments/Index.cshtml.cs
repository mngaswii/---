using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Проверка входа

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Разрешённые роли

            string? role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin" &&
                role != "Master" &&
                role != "Client")
            {
                return RedirectToPage("/Index");
            }

            Appointments = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .ToListAsync();

            return Page();
        }
    }
}