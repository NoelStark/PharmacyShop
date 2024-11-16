using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
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
            builder.Services.AddSingleton<MedicineConfiguration>();
            builder.Services.AddSingleton<MedicationOverviewPageViewModel>();
            builder.Services.AddSingleton<MedicationOverviewPage>();
			Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
			{
#if ANDROID
				h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
			});

			builder.Logging.AddDebug();

#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddSingleton<CheckoutViewModel>();
			builder.Services.AddTransient<CheckoutPage>();
			return builder.Build();
		}
	}


}
