using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public SelectList Clients { get; set; } = default!;

        public SelectList Masters { get; set; } = default!;

        public SelectList Services { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Проверка авторизации

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            LoadLists();

            return Page();
        }

        public IActionResult OnPost()
        {
            // Проверка авторизации

            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                LoadLists();

                return Page();
            }

            // Проверка пересечения записей

            bool isBusy = _context.Appointments.Any(a =>

                a.MasterId == Appointment.MasterId &&

                a.AppointmentDate == Appointment.AppointmentDate
            );

            if (isBusy)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Мастер уже занят на это время");

                LoadLists();

                return Page();
            }

            _context.Appointments.Add(Appointment);

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        private void LoadLists()
        {
            Clients = new SelectList(
                _context.Clients.ToList(),
                "Id",
                "FirstName");

            Masters = new SelectList(
                _context.Masters.ToList(),
                "Id",
                "FirstName");

            Services = new SelectList(
                _context.Services.ToList(),
                "Id",
                "Name");
        }
    }
}