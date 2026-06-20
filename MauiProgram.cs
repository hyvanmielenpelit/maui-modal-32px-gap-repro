using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI.Windowing;
#endif

namespace ModalBugRepro;

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
#if WINDOWS
			.ConfigureLifecycleEvents(events =>
			{
				events.AddWindows(windowsLifecycleBuilder =>
				{
					windowsLifecycleBuilder.OnWindowCreated(window =>
					{
						// Go full-screen, same as GnollHack does
						window.AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
					});
				});
			})
#endif
			;

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
