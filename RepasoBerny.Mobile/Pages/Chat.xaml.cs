using System.Collections.ObjectModel;
namespace RepasoBerny.Mobile.Pages;

public partial class Chat : ContentPage
{
    public ObservableCollection<MessageItem> Messages { get; set; }
    public NotificationItem FirstNotification { get; set; }
    public NotificationItem SecondNotification { get; set; }

    public Chat()
    {
        InitializeComponent();
        LoadNotifications();
        LoadMessages();
        BindingContext = this;
    }

    private void LoadNotifications()
    {
        FirstNotification = new NotificationItem
        {
            Title = "Evento de capacitaci�n",
            Time = "Hace 2 hrs",
            Description = "Nueva capacitaci�n programada para emprendedores.",
            Date = "15/Abril - 15:30 hrs"
        };

        SecondNotification = new NotificationItem
        {
            Title = "Actualizaci�n de plataforma",
            Time = "Ayer",
            Description = "Nuevas funciones disponibles, revisa la gu�a de usuario actualizada."
        };
    }

    private void LoadMessages()
    {
        Messages = new ObservableCollection<MessageItem>
        {
            new MessageItem
            {
                Name = "Maria Rodriguez",
                LastMessage = "Necesito ayuda con el proceso de...",
                Time = "10:14",
                ProfileImage = "maria_profile.png",
                IsUnread = true,
                UnreadCount = 2
            },
            new MessageItem
            {
                Name = "Juan Perez",
                LastMessage = "Gracias por la informaci�n ya revise...",
                Time = "9:04",
                ProfileImage = "juan_profile.png",
                IsUnread = false
            },
            new MessageItem
            {
                Name = "Daniel Garcia",
                LastMessage = "�Cu�ndo es la pr�xima sesi�n?",
                Time = "8:32",
                ProfileImage = "daniel_profile.png",
                IsUnread = true,
                UnreadCount = 1
            }
        };
    }

    private void OnMassCommButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Comunicaci�n Masiva",
                     "Preparando comunicaci�n para todos los microempresarios.",
                     "OK");
    }
}

public class MessageItem
{
    public string Name { get; set; }
    public string LastMessage { get; set; }
    public string Time { get; set; }
    public string ProfileImage { get; set; }
    public bool IsUnread { get; set; }
    public int UnreadCount { get; set; }
}

public class NotificationItem
{
    public string Title { get; set; }
    public string Time { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
}
