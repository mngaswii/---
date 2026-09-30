namespace сайт_курсач.Models
{
    public class Service
    {
        public int Id { get; set; }

        // Название услуги
        public string Name { get; set; }

        // Цена
        public decimal Price { get; set; }

        // Длительность в минутах
        public int DurationMinutes { get; set; }

        // Записи на услугу
        public List<Appointment>? Appointments { get; set; }
    }
}