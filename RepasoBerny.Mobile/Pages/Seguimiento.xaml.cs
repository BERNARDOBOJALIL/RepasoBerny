using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
namespace RepasoBerny.Mobile.Pages
{
    public partial class Seguimiento : ContentPage
    {
        private bool isCandidatosActive = true;
        private Dictionary<string, bool> expandedStates = new Dictionary<string, bool>();

        // Define colores para usar en la aplicación
        private static readonly Color TabSelectedColor = Colors.Transparent;
        private static readonly Color TabUnselectedColor = Colors.Transparent;

        public Seguimiento()
        {
            InitializeComponent();
            // Cargar los datos de candidatos por defecto
            LoadCandidatosData();

            // Suscribirse al evento de cambio de tema
            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                // Recargar los datos para actualizar todos los controles con los nuevos colores
                if (isCandidatosActive)
                    LoadCandidatosData();
                else
                    LoadRegistradosData();
            };
        }

        private async void OnComenzarEncuestaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Survey());
        }

        void OnCandidatosTabTapped(object sender, EventArgs e)
        {
            if (!isCandidatosActive)
            {
                // Hacer que el tab Candidatos se vea seleccionado (más grande)
                CandidatosTab.Scale = 1.1;
                CandidatosTab.BackgroundColor = GetTabBackgroundColor(true);
                CandidatosTab.ZIndex = 1;

                // Hacer que el tab Registrados se vea deseleccionado
                RegistradosTab.Scale = 1.0;
                RegistradosTab.BackgroundColor = TabUnselectedColor;
                RegistradosTab.ZIndex = 0;

                // Guardar el estado de las tarjetas expandidas antes de limpiar
                SaveExpandedStates();

                // Aquí puedes cargar los datos de candidatos
                LoadCandidatosData();
                isCandidatosActive = true;
            }
        }

        void OnRegistradosTabTapped(object sender, EventArgs e)
        {
            if (isCandidatosActive)
            {
                // Hacer que el tab Registrados se vea seleccionado (más grande)
                RegistradosTab.Scale = 1.1;
                RegistradosTab.BackgroundColor = GetTabBackgroundColor(true);
                RegistradosTab.ZIndex = 1;

                // Hacer que el tab Candidatos se vea deseleccionado
                CandidatosTab.Scale = 1.0;
                CandidatosTab.BackgroundColor = TabUnselectedColor;
                CandidatosTab.ZIndex = 0;

                // Guardar el estado de las tarjetas expandidas antes de limpiar
                SaveExpandedStates();

                // Aquí puedes cargar los datos de registrados
                LoadRegistradosData();
                isCandidatosActive = false;
            }
        }

        void OnCardTapped(object sender, TappedEventArgs e)
        {
            // Obtener el ID de la tarjeta desde el CommandParameter
            string cardId = e.Parameter?.ToString();
            if (!string.IsNullOrEmpty(cardId))
            {
                // Buscar el contenido expandible en el layout
                var cardLayout = RegistrosContainer.Children.FirstOrDefault(c => c is VerticalStackLayout vsl && vsl.ClassId == $"RegistroCard{cardId}") as VerticalStackLayout;
                if (cardLayout != null)
                {
                    // Encontrar el contenido expandible dentro de la tarjeta
                    var border = cardLayout.Children.FirstOrDefault() as Border;
                    if (border != null)
                    {
                        var grid = border.Content as Grid;
                        if (grid != null && grid.Children.Count > 1)
                        {
                            var expandedContent = grid.Children[1] as VerticalStackLayout;
                            if (expandedContent != null)
                            {
                                // Alternar visibilidad del contenido expandible
                                expandedContent.IsVisible = !expandedContent.IsVisible;

                                // Buscar la flecha y actualizar su rotación manualmente (para los casos dinámicos)
                                var headerGrid = grid.Children[0] as Grid;
                                if (headerGrid != null)
                                {
                                    // La flecha está en la segunda columna
                                    var arrowLabel = headerGrid.Children.FirstOrDefault(c => c is Label && headerGrid.GetColumn((IView)c) == 1) as Label;
                                    if (arrowLabel != null)
                                    {
                                        // Animar la rotación de la flecha
                                        var targetRotation = expandedContent.IsVisible ? 90 : 0;
                                        arrowLabel.RotateTo(targetRotation, 150, Easing.CubicOut);
                                    }
                                }

                                // Guardar el estado en el diccionario
                                expandedStates[$"ExpandedContent{cardId}"] = expandedContent.IsVisible;
                            }
                        }
                    }
                }
            }
        }

        private void SaveExpandedStates()
        {
            // Guardar el estado de todas las tarjetas actualmente expandidas
            foreach (var child in RegistrosContainer.Children)
            {
                if (child is VerticalStackLayout cardLayout)
                {
                    string cardId = cardLayout.ClassId.Replace("RegistroCard", "");
                    var border = cardLayout.Children.FirstOrDefault() as Border;
                    if (border != null)
                    {
                        var grid = border.Content as Grid;
                        if (grid != null && grid.Children.Count > 1)
                        {
                            var expandedContent = grid.Children[1] as VerticalStackLayout;
                            if (expandedContent != null)
                            {
                                expandedStates[$"ExpandedContent{cardId}"] = expandedContent.IsVisible;
                            }
                        }
                    }
                }
            }
        }

        private void LoadCandidatosData()
        {
            // Limpiar el contenedor
            RegistrosContainer.Clear();

            // Cargar candidatos de tu fuente de datos
            var candidatos = GetCandidatosFromDataSource();

            // Agregar cada candidato como una tarjeta
            foreach (var candidato in candidatos)
            {
                RegistrosContainer.Add(CreateRegistroCard(
                    candidato.Id,
                    candidato.Nombre,
                    candidato.Direccion,
                    candidato.FechaVisita,
                    candidato.Estado,
                    candidato.ProximaCita,
                    true
                ));
            }
        }

        private void LoadRegistradosData()
        {
            // Limpiar el contenedor
            RegistrosContainer.Clear();

            // Cargar registrados de tu fuente de datos
            var registrados = GetRegistradosFromDataSource();

            // Agregar cada registrado como una tarjeta
            foreach (var registrado in registrados)
            {
                RegistrosContainer.Add(CreateRegistroCard(
                    registrado.Id,
                    registrado.Nombre,
                    registrado.Direccion,
                    registrado.FechaVisita,
                    registrado.Estado,
                    registrado.ProximaCita,
                    false
                ));
            }
        }

        // Este método es solo para simular datos - reemplázalo con tu lógica real
        private List<RegistroModel> GetCandidatosFromDataSource()
        {
            // Simular algunos datos de candidatos
            return new List<RegistroModel>
            {
                new RegistroModel {
                    Id = "1",
                    Nombre = "tortillas moi",
                    Direccion = "Calle Principal #123",
                    FechaVisita = "3-10-2025",
                    Estado = "pendiente",
                    ProximaCita = "Lunes 29 4:00pm"
                },
                new RegistroModel {
                    Id = "2",
                    Nombre = "Panadería El Trigal",
                    Direccion = "Av. Reforma #456",
                    FechaVisita = "5-10-2025",
                    Estado = "rechazado",
                    ProximaCita = "Martes 30 10:00am"
                }
            };
        }

        // Este método es solo para simular datos - reemplázalo con tu lógica real
        private List<RegistroModel> GetRegistradosFromDataSource()
        {
            // Simular algunos datos de registrados
            return new List<RegistroModel>
            {
                new RegistroModel {
                    Id = "3",
                    Nombre = "Taquería Don Juan",
                    Direccion = "Plaza Central #78",
                    FechaVisita = "1-10-2025",
                    Estado = "completado",
                    ProximaCita = "Jueves 25 3:30pm"
                },
                new RegistroModel {
                    Id = "4",
                    Nombre = "Zapatería El Paso",
                    Direccion = "Calzada Norte #234",
                    FechaVisita = "2-10-2025",
                    Estado = "completado",
                    ProximaCita = "Viernes 26 11:00am"
                }
            };
        }

        // Métodos auxiliares para obtener colores según el tema
        private Color GetTabBackgroundColor(bool isSelected)
        {
            if (!isSelected)
                return TabUnselectedColor;

            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Color.FromHex("#8A7A00")
                : Color.FromHex("#FFEEAA");
        }

        private Color GetCardBackgroundColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Color.FromHex("#242424")
                : Colors.White;
        }

        private Color GetCardStrokeColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Colors.DimGray
                : Colors.LightGray;
        }

        private Color GetTextColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Colors.White
                : Colors.Black;
        }

        private Color GetSecondaryTextColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Colors.LightGray
                : Colors.Black;
        }

        private Color GetExpandedBackgroundColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Color.FromHex("#2A2A2A")
                : Color.FromHex("#FBF9F1");
        }

        private Color GetHighlightColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Color.FromHex("#8A7A00")
                : Color.FromHex("#FFEEAA");
        }

        private Color GetInputBackgroundColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Color.FromHex("#333333")
                : Colors.White;
        }

        private Color GetPlaceholderColor()
        {
            return Application.Current.RequestedTheme == AppTheme.Dark
                ? Colors.DarkGray
                : Colors.Gray;
        }

        // Para crear tarjetas dinámicamente
        private VerticalStackLayout CreateRegistroCard(string id, string nombre, string direccion,
                                  string visitado, string estado, string proximaCita, bool isCandidato)
        {
            // Crear un nuevo VerticalStackLayout para la tarjeta
            var cardLayout = new VerticalStackLayout
            {
                ClassId = $"RegistroCard{id}"
            };

            // Crear la border para la tarjeta
            var border = new Border
            {
                StrokeShape = new Rectangle(),
                Stroke = GetCardStrokeColor(),
                StrokeThickness = 1,
                BackgroundColor = GetCardBackgroundColor()
            };

            // Crear el grid principal
            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Crear el grid para el contenido visible
            var visibleGrid = new Grid
            {
                Padding = new Thickness(15)
            };
            visibleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            visibleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // Crear el StackLayout para la información del registro
            var infoStack = new VerticalStackLayout { Spacing = 5 };
            infoStack.Add(new Label
            {
                Text = nombre,
                FontAttributes = FontAttributes.Bold,
                TextColor = GetTextColor()
            });
            infoStack.Add(new Label
            {
                Text = direccion,
                TextColor = GetSecondaryTextColor()
            });
            infoStack.Add(new Label
            {
                Text = $"Visitado: {visitado}",
                Margin = new Thickness(0, 5, 0, 0),
                TextColor = GetSecondaryTextColor()
            });
            infoStack.Add(new Label
            {
                Text = $"Estado: {estado}",
                TextColor = GetSecondaryTextColor()
            });
            infoStack.Add(new Label
            {
                Text = $"Hora próximo: {proximaCita}",
                TextColor = GetSecondaryTextColor()
            });
            visibleGrid.Add(infoStack, 0, 0);

            // Crear la flecha con una etiqueta
            var arrowLabel = new Label
            {
                Text = ">",
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray,
                ClassId = $"ArrowIcon{id}" // Para identificarlo fácilmente
            };
            visibleGrid.Add(arrowLabel, 1, 0);

            // Crear el contenido expandible
            var expandedContent = new VerticalStackLayout
            {
                IsVisible = expandedStates.ContainsKey($"ExpandedContent{id}") ? expandedStates[$"ExpandedContent{id}"] : false,
                BackgroundColor = GetExpandedBackgroundColor(),
                ClassId = $"ExpandedContent{id}"
            };

            // Si el contenido está expandido, girar la flecha
            if (expandedContent.IsVisible)
            {
                arrowLabel.Rotation = 90;
            }

            // Dropdown para actualizar estado
            var estadoGrid = new Grid { Margin = new Thickness(0, 10, 0, 0) };
            var estadoBorder = new Border
            {
                Margin = new Thickness(15, 0),
                BackgroundColor = GetHighlightColor(),
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(5) }
            };
            // Declarar los controles que se actualizarán dinámicamente
            var notaLabel = new Label
            {
                Text = "Agregar Nota:",
                FontSize = 16,
                TextColor = GetTextColor(),
                Margin = new Thickness(15, 15, 15, 5)
            };

            var notaEditor = new Editor
            {
                Placeholder = "Escribe una nota aquí...",
                PlaceholderColor = GetPlaceholderColor(),
                TextColor = GetTextColor(),
                AutoSize = EditorAutoSizeOption.TextChanges,
                Margin = new Thickness(5)
            };

            var notaBorder = new Border
            {
                Margin = new Thickness(15, 0, 15, 10),
                StrokeShape = new Rectangle(),
                Stroke = GetCardStrokeColor(),
                StrokeThickness = 1,
                HeightRequest = 100,
                BackgroundColor = GetInputBackgroundColor(),
                Content = notaEditor
            };

            // Picker de estado
            var estadoPicker = new Picker
            {
                Title = "Actualizar Estado",
                FontSize = 16,
                Margin = new Thickness(10, 5),
                TextColor = GetTextColor(),
                TitleColor = GetTextColor()
            };

            if (isCandidato)
            {
                estadoPicker.Items.Add("No interesado por el momento");
                estadoPicker.Items.Add("Pendiente de alta");
                estadoPicker.Items.Add("Programado");
            }
            else
            {
                estadoPicker.Items.Add("Cancelar participación");
                estadoPicker.Items.Add("Pendiente de encuesta");
                estadoPicker.Items.Add("Requiere seguimiento");
            }

            // Evento para actualizar el label y placeholder
            estadoPicker.SelectedIndexChanged += (s, e) =>
            {
                var seleccionado = estadoPicker.SelectedItem?.ToString();

                if (seleccionado == "Cancelar participación")
                {
                    notaLabel.Text = "Motivo de cancelación:";
                    notaEditor.Placeholder = "Describe el motivo...";
                }
                else
                {
                    notaLabel.Text = "Agregar Nota:";
                    notaEditor.Placeholder = "Escribe una nota aquí...";
                }
            };

            // Agregar a la vista
            estadoBorder.Content = estadoPicker;
            estadoGrid.Add(estadoBorder);
            expandedContent.Add(estadoGrid);
            expandedContent.Add(notaLabel);
            expandedContent.Add(notaBorder);


            // Sección para programar cita
            expandedContent.Add(new Label
            {
                Text = "Programar Próxima Cita",
                FontSize = 16,
                TextColor = GetTextColor(),
                Margin = new Thickness(15, 15, 15, 5)
            });

            // Selector de fecha
            var dateBorder = new Border
            {
                Margin = new Thickness(15, 0),
                StrokeShape = new Rectangle(),
                Stroke = GetCardStrokeColor(),
                StrokeThickness = 1,
                BackgroundColor = GetInputBackgroundColor()
            };
            var datePicker = new DatePicker
            {
                Format = "D",
                MinimumDate = DateTime.Today,
                TextColor = GetTextColor(),
                Margin = new Thickness(10, 5)
            };
            dateBorder.Content = datePicker;
            expandedContent.Add(dateBorder);

            // Selector de hora
            var timeBorder = new Border
            {
                Margin = new Thickness(15, 10, 15, 0),
                StrokeShape = new Rectangle(),
                Stroke = GetCardStrokeColor(),
                StrokeThickness = 1,
                BackgroundColor = GetInputBackgroundColor()
            };
            var timePicker = new TimePicker
            {
                Format = "t",
                TextColor = GetTextColor(),
                Margin = new Thickness(10, 5)
            };
            timeBorder.Content = timePicker;
            expandedContent.Add(timeBorder);


            // Botón para guardar
            var guardarButton = new Button
            {
                Text = "Guardar visita",
                BackgroundColor = GetHighlightColor(),
                TextColor = GetTextColor(),
                CornerRadius = 5,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                Margin = new Thickness(0, 20, 0, 20)
            };

            if (!isCandidato)
            {
                var Encuesta = new Button
                {
                    Text = "Comenzar encuesta",
                    BackgroundColor = GetHighlightColor(),
                    TextColor = GetTextColor(),
                    CornerRadius = 5,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 200,
                    Margin = new Thickness(0, 20, 0, 20)
                };

                Encuesta.Clicked += async (s, e) =>
                {
                    await Navigation.PushAsync(new Survey());
                };

                expandedContent.Add(Encuesta);
            }


            expandedContent.Add(guardarButton);


            // Agregar todo al grid principal
            mainGrid.Add(visibleGrid, 0, 0);
            mainGrid.Add(expandedContent, 0, 1);

            // Agregar el grid a la border
            border.Content = mainGrid;

            // Agregar la border al layout de la tarjeta
            cardLayout.Add(border);

            // Agregar el gestor de toques
            var tapGesture = new TapGestureRecognizer
            {
                CommandParameter = id
            };
            tapGesture.Tapped += OnCardTapped;
            cardLayout.GestureRecognizers.Add(tapGesture);

            return cardLayout;
        }
    }

    // Modelo simple para los datos de registro
    public class RegistroModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string FechaVisita { get; set; }
        public string Estado { get; set; }
        public string ProximaCita { get; set; }
    }
}