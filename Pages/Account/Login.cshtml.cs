using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Security;

namespace сайт_курсач.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == Login);

            if (user == null || !PasswordHasher.Verify(Password, user.Password))
            {
                ErrorMessage = "Неверный логин или пароль";

                return Page();
            }

            HttpContext.Session.SetString(
                "UserLogin",
                user.Login);

            // Старый аккаунт с открытым паролем автоматически переводим на хеш.
            if (!user.Password.StartsWith("PBKDF2$", StringComparison.Ordinal))
            {
                user.Password = PasswordHasher.Hash(Password);
                _context.SaveChanges();
            }

            HttpContext.Session.SetString(
                "UserRole",
                user.Role.Name);

            return RedirectToPage("/Index");
        }
    }
}