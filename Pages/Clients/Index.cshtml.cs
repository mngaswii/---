using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Client> Clients { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Проверка входа

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Получаем роль

            string? role = HttpContext.Session.GetString("UserRole");

            // Проверка роли

            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            // Загрузка клиентов

            Clients = await _context.Clients.ToListAsync();

            return Page();
        }
    }
}