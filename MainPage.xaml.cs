namespace ModalBugRepro;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnPushModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new ModalPage());
	}
}
