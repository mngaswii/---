using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        public Client Client { get; set; }

        public void OnGet(int id)
        {
            // временные данные
            Client = new Client
            {
                Id = id,
                FirstName = "Анна",
                PhoneNumber = "123456"
            };
        }
    }
}