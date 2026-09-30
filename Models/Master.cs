namespace сайт_курсач.Models
{
    public class Master
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        // Специализация
        public string Specialization { get; set; }

        // Записи мастера
        public List<Appointment>? Appointments { get; set; }
    }
}