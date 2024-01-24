using Microsoft.Extensions.Logging;

namespace ToDoMauiApp
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            string dbPath = FileAccessHelper.GetLocalFilePath("todos.db3");
            builder.Services.AddSingleton<ToDoRepository>(s => ActivatorUtilities.CreateInstance<ToDoRepository>(s, dbPath));

            return builder.Build();
        }
    }
}
