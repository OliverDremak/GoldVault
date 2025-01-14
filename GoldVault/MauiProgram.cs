using CommunityToolkit.Maui;
using GoldVault.Pages;
using GoldVault.Services;
using GoldVault.ViewModels;
using Microsoft.Extensions.Logging;

namespace GoldVault
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa_solid.ttf", "FontAwesome");
                });
            //Pages+ViewModels
            builder.Services.AddSingleton<MainPage>();  
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<HistoryPage>();
            builder.Services.AddSingleton<HistoryViewModel>();
            builder.Services.AddSingleton<StockPage>();
            builder.Services.AddSingleton<StockViewModel>();
            //Databases
            string dbPathforStock = Path.Combine(FileSystem.AppDataDirectory, "Stock.db");
            builder.Services.AddSingleton<StockDatabase>(s => new StockDatabase(dbPathforStock));
            string dbPathforTransact = Path.Combine(FileSystem.AppDataDirectory, "Transactions.db");
            builder.Services.AddSingleton<TransactionDatabase>(s => new TransactionDatabase(dbPathforTransact));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
