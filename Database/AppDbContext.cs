using Microsoft.EntityFrameworkCore;
using NutikasPaevik.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutikasPaevik.Database
{
    public class AppDbContext : DbContext
    {
        // Таблица заметок
        public DbSet<Note> Notes { get; set; }

        // Конструктор для внедрения опций
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Настройка модели и начальные данные
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ограничение длины StickerImage
            modelBuilder.Entity<Note>().Property(n => n.StickerImage).HasMaxLength(100);
            // Title и Content обязательны
            modelBuilder.Entity<Note>().Property(n => n.Title).IsRequired();
            modelBuilder.Entity<Note>().Property(n => n.Content).IsRequired();

            // Начальные данные для заметок
            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id = 1,
                    Title = "Aboba",
                    Content = "Aboba Aboba",
                    CreationTime = DateTime.Now.AddDays(-2),
                    RotationAngle = 2.5,
                    StickerImage = "greennote.png"
                },
                new Note
                {
                    Id = 2,
                    Title = "Super aboba",
                    Content = "Aboba Aboba aksdjjkasdkans dnajdkakjsdnakj nsd kjnaskjdn akjnsd jkansjkdnakjsn dkjandkj",
                    CreationTime = DateTime.Now.AddDays(-1),
                    RotationAngle = -1.7,
                    StickerImage = "bluenote.png"
                },
                new Note
                {
                    Id = 3,
                    Title = "Aboba Aboba Aboba",
                    Content = "Aboba aksdjjkasdkans dnajdkakjsdnakj nsd kjnaskjdn akjnsd jkansjkdnakjsn dkjandkj asjdlökasdkandka ndklans ldnaks ndalns dkans",
                    CreationTime = DateTime.Now,
                    RotationAngle = 4.0,
                    StickerImage = "rednote.png"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
