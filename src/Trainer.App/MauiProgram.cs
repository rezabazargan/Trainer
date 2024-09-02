using Microsoft.Extensions.Logging;
using Trainer.App.Services;

namespace Trainer.App
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
                });

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ApplicationSettings.BaseUrl)
            };
            builder.Services.AddSingleton(httpClient);
            builder.Services.AddSingleton<AuthenticationService>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
