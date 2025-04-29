using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using System;
using NutikasPaevik.Database;
using NutikasPaevik.Pages.Views;

namespace NutikasPaevik
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            // Регистрация DbContext с SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"), ServiceLifetime.Scoped);

            // Регистрация DiaryViewModel
            builder.Services.AddSingleton<DiaryViewModel>();

            // Логирование для отладки
#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Пересоздание базы данных
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            return app;
        }
    }
}
