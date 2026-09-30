using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Masters
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Master> Masters { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Проверка входа

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Admin и Master

            string? role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin" && role != "Master")
            {
                return RedirectToPage("/Index");
            }

            Masters = await _context.Masters.ToListAsync();

            return Page();
        }
    }
}