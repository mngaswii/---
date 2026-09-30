using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Services
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public IActionResult OnGet(int? id)
        {
            // Только Admin

            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            Service = _context.Services.Find(id);

            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            // Только Admin

            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var service = _context.Services.Find(id);

            if (service != null)
            {
                _context.Services.Remove(service);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}