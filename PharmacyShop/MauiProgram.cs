using Microsoft.Extensions.Logging;
using PharmacyShop.ViewModels;
using PharmacyShop.Views;

namespace PharmacyShop
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
			builder.Services.AddSingleton<CheckoutViewModel>();
			builder.Services.AddTransient<CheckoutPage>();
			return builder.Build();
		}
	}
}
