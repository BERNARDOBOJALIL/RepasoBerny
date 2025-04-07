namespace RepasoBerny.Mobile.Pages;

public partial class Offline : ContentPage
{
	public Offline()
	{
		InitializeComponent();
	}

    private async void OnScreenTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Chat()); 
    }
}