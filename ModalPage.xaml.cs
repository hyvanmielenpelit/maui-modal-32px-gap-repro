namespace ModalBugRepro;

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
}
