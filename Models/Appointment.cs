namespace сайт_курсач.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // Клиент
        public int ClientId { get; set; }

        public Client Client { get; set; }

        // Мастер
        public int MasterId { get; set; }

        public Master Master { get; set; }

        // Услуга
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        // Дата и время записи
        public DateTime AppointmentDate { get; set; }

        // Статус записи
        public string Status { get; set; }

        // Комментарий
        public string? Notes { get; set; }
    }
}