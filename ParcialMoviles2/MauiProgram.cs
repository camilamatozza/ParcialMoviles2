using Microsoft.Extensions.Logging;
using PaisesApp;
using PaisesApp.Services;
using PaisesApp.ViewModels;

namespace ParcialMoviles2;

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

        // Servicios
        builder.Services.AddSingleton<IPaisService, PaisService>();
        builder.Services.AddSingleton<IFavoritosRepository, FavoritosRepository>();

        // ViewModels
        builder.Services.AddSingleton<PaisesViewModel>();

        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SensoresPage>();

        return builder.Build();
    }
}