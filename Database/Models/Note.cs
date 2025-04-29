using System.ComponentModel.DataAnnotations;

namespace NutikasPaevik.Database
{
    public class Note
    {
        // Первичный ключ для EF Core
        [Key]
        public int Id { get; set; }

        // Время создания заметки
        public DateTime? CreationTime { get; set; }

        // Заголовок заметки
        public string Title { get; set; }

        // Содержимое заметки
        public string Content { get; set; }

        // Угол наклона стикера (от -5 до 5 градусов)
        public double? RotationAngle { get; set; }

        // Путь к изображению стикера
        public string StickerImage { get; set; }
    }
}
