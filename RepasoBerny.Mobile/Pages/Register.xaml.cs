namespace RepasoBerny.Mobile.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}
    private void PhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string text = entry.Text.Replace("+", "");

        if (text.Length > 4)
            text = text.Substring(0, 4);
        entry.Text = "+" + text;
    }
    private async void OnScreenTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Seguimiento());
    }
}