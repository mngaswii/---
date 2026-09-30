using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;
using сайт_курсач.Security;

namespace сайт_курсач.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public RegisterInput Input { get; set; } = new();

        public class RegisterInput
        {
            [Required(ErrorMessage = "Введите имя")]
            public string FirstName { get; set; } = "";

            [Required(ErrorMessage = "Введите фамилию")]
            public string LastName { get; set; } = "";

            [Required(ErrorMessage = "Введите телефон")]
            public string PhoneNumber { get; set; } = "";

            [Required(ErrorMessage = "Введите email")]
            [EmailAddress(ErrorMessage = "Введите корректный email")]
            public string Email { get; set; } = "";

            [Required(ErrorMessage = "Введите логин")]
            [MinLength(3, ErrorMessage = "Логин должен содержать минимум 3 символа")]
            public string Login { get; set; } = "";

            [Required(ErrorMessage = "Введите пароль")]
            [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
            public string Password { get; set; } = "";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Input.Login = Input.Login.Trim();
            Input.Email = Input.Email.Trim().ToLowerInvariant();

            if (await _context.Users.AnyAsync(u => u.Login == Input.Login))
            {
                ModelState.AddModelError("Input.Login", "Этот логин уже занят");
                return Page();
            }

            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == Input.Email))
            {
                ModelState.AddModelError("Input.Email", "Этот email уже зарегистрирован");
                return Page();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Client");
            if (role == null)
            {
                role = new Role { Name = "Client" };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }

            var client = new Client
            {
                FirstName = Input.FirstName.Trim(),
                LastName = Input.LastName.Trim(),
                PhoneNumber = Input.PhoneNumber.Trim(),
                Email = Input.Email
            };

            var user = new User
            {
                Login = Input.Login,
                Password = PasswordHasher.Hash(Input.Password),
                Email = Input.Email,
                RoleId = role.Id
            };

            _context.Clients.Add(client);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetString("UserRole", role.Name);

            return RedirectToPage("/Index");
        }
    }
}
