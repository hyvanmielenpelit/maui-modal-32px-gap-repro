namespace ModalBugRepro;
#if WINDOWS
using System.Reflection;
using Microsoft.Maui.Platform;
#endif

public partial class ModalPage : ContentPage
{
	private int _clickCount = 0;

	public ModalPage()
	{
		InitializeComponent();
	}

	private void OnTopButtonClicked(object? sender, EventArgs e)
	{
		_clickCount++;
		ClickCountLabel.Text = $"Top button clicked: {_clickCount} time{(_clickCount != 1 ? "s" : "")}";
	}

	private async void OnPopModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}

/* Below is the suggested workaround/fix for the problem! Please uncomment it to try out! */

/*
        protected override void OnAppearing()
        {
            base.OnAppearing();
    #if WINDOWS
            // Defer to ensure the platform visual tree is fully built
            Dispatcher.Dispatch(() =>
            {
                FixModalTitleBarGap(this);
            });
    #endif
        }
    #if WINDOWS
    static void FixModalTitleBarGap(ContentPage modalPage)
        {
            var mauiContext = modalPage.Handler?.MauiContext;
            if (mauiContext is null)
                return;
            // 1. Get the NavigationRootManager for this modal's scoped context
            var navManager = mauiContext.Services.GetService(
                typeof(Microsoft.Maui.Platform.NavigationRootManager));
            if (navManager is not null)
            {
                // 2. Invoke internal void SetTitleBarVisibility(bool isVisible)
                var method = navManager.GetType().GetMethod(
                    "SetTitleBarVisibility", 
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (method is not null)
                {
                    // Passing false tells it to collapse the title bar, zero the 32px margin,
                    // and clear the unclickable non-client input regions.
                    method.Invoke(navManager, new object[] { false });
                }
            }
        }
#endif
*/
}
