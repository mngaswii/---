using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;

namespace сайт_курсач.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int ClientsCount { get; set; }

        public int MastersCount { get; set; }

        public int ServicesCount { get; set; }

        public int AppointmentsCount { get; set; }

        public IActionResult OnGet()
        {
            // Проверка входа

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Только Admin

            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            ClientsCount = _context.Clients.Count();

            MastersCount = _context.Masters.Count();

            ServicesCount = _context.Services.Count();

            AppointmentsCount = _context.Appointments.Count();

            return Page();
        }
    }
}