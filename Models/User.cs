namespace сайт_курсач.Models
{
    public class User
    {
        public int Id { get; set; }

        // Логин
        public string Login { get; set; }

        // Пароль
        public string Password { get; set; }

        // Email
        public string Email { get; set; }

        // Роль
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}