using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using PharmacyShop.Services;
using PharmacyShop.ViewModels;
using PharmacyShop.Views;
using PharmacyShop.Views.Checkout;
using CommunityToolkit.Maui;


namespace PharmacyShop
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit();
           

            builder.Services.AddSingleton<MedicineConfiguration>();

            builder.Services.AddSingleton<MedicationOverviewPageViewModel>();
            builder.Services.AddSingleton<MedicationOverviewPage>();

			builder.Services.AddTransient<MedicationDetailsViewModel>();
			builder.Services.AddTransient<MedicationDetailsPage>();

			builder.Services.AddTransient<CheckoutViewModel>();
			builder.Services.AddTransient<CheckoutPage>();

			builder.Services.AddSingleton<PersonalInfoViewModel>();
			builder.Services.AddTransient<PersonalInfoPage>();

			builder.Services.AddTransient<PaymentInfoViewModel>();
			builder.Services.AddTransient<PaymentInfoPage>();

			builder.Services.AddTransient<ConfirmationViewModel>();
			builder.Services.AddTransient<ConfirmationPage>();

			builder.Services.AddSingleton<PersonService>();
			builder.Services.AddSingleton<MedicineService>();

			builder.Services.AddTransient<PopupView>();
			builder.Services.AddTransient<PopupViewModel>();
			Preferences.Clear();

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
			
			return builder.Build();
		}
	}


}
