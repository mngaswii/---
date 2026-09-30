using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Masters
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Master Master { get; set; }

        public IActionResult OnGet(int? id)
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

            if (id == null)
            {
                return NotFound();
            }

            Master = _context.Masters.Find(id);

            if (Master == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Masters.Update(Master);

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}