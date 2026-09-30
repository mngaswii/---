using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Clients
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Client Client { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // потом добавим БД

            return RedirectToPage("Index");
        }
    }
}