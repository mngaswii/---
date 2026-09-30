namespace сайт_курсач.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        // Записи клиента
        public List<Appointment>? Appointments { get; set; }
    }
}