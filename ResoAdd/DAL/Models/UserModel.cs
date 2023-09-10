using System.ComponentModel.DataAnnotations;

namespace ResoAdd.DAL.Models
{
    public class UserModel
    {
        /// <summary>
        /// Создание модели Юзера для базы данных
        /// </summary>
        [Key]
        public int? UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int? Status { get; set; } = 0;

    }
}

