namespace сайт_курсач.Models
{
    public class Payment
    {
        public int Id { get; set; }

        // Запись
        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        // Сумма
        public decimal Amount { get; set; }

        // Дата оплаты
        public DateTime PaymentDate { get; set; }

        // Способ оплаты
        public string PaymentMethod { get; set; }

        // Статус оплаты
        public string Status { get; set; }
    }
}