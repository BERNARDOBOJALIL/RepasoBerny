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
            Title = "Evento de capacitación",
            Time = "Hace 2 hrs",
            Description = "Nueva capacitación programada para emprendedores.",
            Date = "15/Abril - 15:30 hrs"
        };

        SecondNotification = new NotificationItem
        {
            Title = "Actualización de plataforma",
            Time = "Ayer",
            Description = "Nuevas funciones disponibles, revisa la guía de usuario actualizada."
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
                LastMessage = "Gracias por la información ya revise...",
                Time = "9:04",
                ProfileImage = "juan_profile.png",
                IsUnread = false
            },
            new MessageItem
            {
                Name = "Daniel Garcia",
                LastMessage = "¿Cuándo es la próxima sesión?",
                Time = "8:32",
                ProfileImage = "daniel_profile.png",
                IsUnread = true,
                UnreadCount = 1
            }
        };
    }

    private void OnMassCommButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Comunicación Masiva",
                     "Preparando comunicación para todos los microempresarios.",
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
