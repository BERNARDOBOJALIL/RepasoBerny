using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RepasoBerny.Mobile.Pages;

public partial class Chat : ContentPage, INotifyPropertyChanged
{
    public ObservableCollection<NotificationItem> NotificationList { get; set; }
    public ICommand MarkAsReadCommand { get; }
    public ICommand DeleteNotificationCommand { get; }

    public Chat()
    {
        InitializeComponent();
        LoadNotifications();
        MarkAsReadCommand = new RelayCommand<NotificationItem>(MarkAsRead);
        DeleteNotificationCommand = new RelayCommand<NotificationItem>(DeleteNotification);
        BindingContext = this;
    }

    public void LoadNotifications()
    {
        NotificationList = new ObservableCollection<NotificationItem>
        {
            new NotificationItem
            {
                Title = "Evento de capacitación",
                Time = "Hace 2 hrs",
                Description = "Nueva capacitación programada para emprendedores.",
                Date = "15/Abril - 15:30 hrs",
                Category = "Eventos",
                IsRead = false
            },
            new NotificationItem
            {
                Title = "Actualización de plataforma",
                Time = "Ayer",
                Description = "Nuevas funciones disponibles, revisa la guía de usuario actualizada.",
                Category = "Actualizaciones",
                IsRead = false
            },
            new NotificationItem
            {
                Title = "Aviso importante",
                Time = "Hace 1 día",
                Description = "Por favor, revisa el nuevo reglamento interno.",
                Category = "Avisos",
                IsRead = false
            },
            new NotificationItem
            {
                Title = "Reunión semanal",
                Time = "Hace 3 días",
                Description = "Reunión semanal de seguimiento de proyectos.",
                Date = "12/Abril - 10:00 hrs",
                Category = "Eventos",
                IsRead = true
            },
            new NotificationItem
            {
                Title = "Promoción especial",
                Time = "Hace 1 semana",
                Description = "Promoción especial para nuevos clientes.",
                Category = "Actualizaciones",
                IsRead = false
            },
            new NotificationItem
            {
                Title = "Mantenimiento preventivo",
                Time = "Hace 2 semanas",
                Description = "Mantenimiento preventivo del sistema.",
                Category = "Avisos",
                IsRead = true
            }
        };

        // Mensaje de depuración
        System.Diagnostics.Debug.WriteLine($"Notificaciones cargadas: {NotificationList.Count}");

        NotificationsListView.ItemsSource = NotificationList;

        // Mensaje de depuración adicional
        System.Diagnostics.Debug.WriteLine($"ItemsSource asignado: {NotificationList.Count}");
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var filteredNotifications = NotificationList.Where(n => n.Title.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase) ||
                                                            n.Description.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        NotificationsListView.ItemsSource = new ObservableCollection<NotificationItem>(filteredNotifications);
    }

    private void OnFilterPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedCategory = FilterPicker.SelectedItem as string;
        if (selectedCategory == "Todas")
        {
            NotificationsListView.ItemsSource = NotificationList;
        }
        else
        {
            var filteredNotifications = NotificationList.Where(n => n.Category == selectedCategory);
            NotificationsListView.ItemsSource = new ObservableCollection<NotificationItem>(filteredNotifications);
        }
    }

    private void OnSortPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedSortOption = SortPicker.SelectedItem as string;
        if (selectedSortOption == "Más reciente primero")
        {
            var sortedNotifications = NotificationList.OrderByDescending(n => n.Time);
            NotificationsListView.ItemsSource = new ObservableCollection<NotificationItem>(sortedNotifications);
        }
        else if (selectedSortOption == "Más antiguo primero")
        {
            var sortedNotifications = NotificationList.OrderBy(n => n.Time);
            NotificationsListView.ItemsSource = new ObservableCollection<NotificationItem>(sortedNotifications);
        }
    }

    private void MarkAsRead(NotificationItem notification)
    {
        if (notification != null)
        {
            notification.IsRead = true;
            OnPropertyChanged(nameof(NotificationList));
        }
    }

    private void DeleteNotification(NotificationItem notification)
    {
        if (notification != null)
        {
            NotificationList.Remove(notification);
        }
    }

    private void OnMassCommButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Comunicación Masiva",
                     "Preparando comunicación para todos los microempresarios.",
                     "OK");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class NotificationItem : ObservableObject
{
    private string? _title;
    private string? _time;
    private string? _description;
    private string? _date;
    private string? _category;
    private bool _isRead;
    private string? _backgroundColor;
    private Color? _titleColor;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public bool IsRead
    {
        get => _isRead;
        set
        {
            SetProperty(ref _isRead, value);
            BackgroundColor = value ? "#FFFFFF" : "#E6F7FF";
            TitleColor = value ? Colors.Black : Colors.Blue;
        }
    }

    public string BackgroundColor
    {
        get => _backgroundColor;
        set => SetProperty(ref _backgroundColor, value);
    }

    public Color TitleColor
    {
        get => _titleColor;
        set => SetProperty(ref _titleColor, value);
    }
}