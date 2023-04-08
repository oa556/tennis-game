using Microsoft.Extensions.Logging;
using TennisGame.Client.Pages;
using TennisGame.Client.Services;

namespace TennisGame.Client
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

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient<IDataService, DataService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddSingleGame>();
            builder.Services.AddTransient<AddDoubleGame>();

            return builder.Build();
        }
    }
}