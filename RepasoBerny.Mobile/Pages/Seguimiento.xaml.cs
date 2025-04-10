// PARTE 2: Code-behind corregido - Seguimiento.xaml.cs
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

        public Seguimiento()
        {
            InitializeComponent();

            // Cargar los datos de candidatos por defecto
            LoadCandidatosData();
        }

        void OnCandidatosTabTapped(object sender, EventArgs e)
        {
            if (!isCandidatosActive)
            {
                // Hacer que el tab Candidatos se vea seleccionado (más grande)
                CandidatosTab.Scale = 1.1;
                CandidatosTab.BackgroundColor = Color.FromHex("#FFEEAA");
                CandidatosTab.ZIndex = 1;

                // Hacer que el tab Registrados se vea deseleccionado
                RegistradosTab.Scale = 1.0;
                RegistradosTab.BackgroundColor = Colors.Transparent;
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
                RegistradosTab.BackgroundColor = Color.FromHex("#FFEEAA");
                RegistradosTab.ZIndex = 1;

                // Hacer que el tab Candidatos se vea deseleccionado
                CandidatosTab.Scale = 1.0;
                CandidatosTab.BackgroundColor = Colors.Transparent;
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
                    candidato.ProximaCita
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
                    registrado.ProximaCita
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

        // Para crear tarjetas dinámicamente
        private VerticalStackLayout CreateRegistroCard(string id, string nombre, string direccion,
                                      string visitado, string estado, string proximaCita)
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
                Stroke = Colors.LightGray,
                StrokeThickness = 1
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

            infoStack.Add(new Label { Text = nombre, FontAttributes = FontAttributes.Bold });
            infoStack.Add(new Label { Text = direccion });
            infoStack.Add(new Label { Text = $"Visitado: {visitado}", Margin = new Thickness(0, 5, 0, 0) });
            infoStack.Add(new Label { Text = $"Estado: {estado}" });
            infoStack.Add(new Label { Text = $"Hora próximo: {proximaCita}" });

            visibleGrid.Add(infoStack, 0, 0);
            visibleGrid.Add(new Label
            {
                Text = ">",
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Colors.Gray
            }, 1, 0);

            // Crear el contenido expandible
            var expandedContent = new VerticalStackLayout
            {
                IsVisible = expandedStates.ContainsKey($"ExpandedContent{id}") ? expandedStates[$"ExpandedContent{id}"] : false,
                BackgroundColor = Color.FromHex("#FBF9F1"),
                ClassId = $"ExpandedContent{id}"
            };

            // Dropdown para actualizar estado
            var estadoGrid = new Grid { Margin = new Thickness(0, 10, 0, 0) };
            var estadoBorder = new Border
            {
                Margin = new Thickness(15, 0),
                BackgroundColor = Color.FromHex("#FFEEAA"),
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(5) }
            };

            var estadoPicker = new Picker
            {
                Title = "Actualizar Estado",
                FontSize = 16,
                Margin = new Thickness(10, 5)
            };
            estadoPicker.Items.Add("No interesado por el momento");
            estadoPicker.Items.Add("Pendiente de alta");
            estadoPicker.Items.Add("Completado");
            estadoPicker.Items.Add("Programado");

            estadoBorder.Content = estadoPicker;
            estadoGrid.Add(estadoBorder);
            expandedContent.Add(estadoGrid);

            // Sección para programar cita
            expandedContent.Add(new Label
            {
                Text = "Programar Próxima Cita",
                FontSize = 16,
                Margin = new Thickness(15, 15, 15, 5)
            });

            // Selector de fecha
            var dateBorder = new Border
            {
                Margin = new Thickness(15, 0),
                StrokeShape = new Rectangle(),
                Stroke = Colors.LightGray,
                StrokeThickness = 1
            };

            var datePicker = new DatePicker
            {
                Format = "D",
                MinimumDate = DateTime.Today,
                Margin = new Thickness(10, 5)
            };

            dateBorder.Content = datePicker;
            expandedContent.Add(dateBorder);

            // Selector de hora
            var timeBorder = new Border
            {
                Margin = new Thickness(15, 10, 15, 0),
                StrokeShape = new Rectangle(),
                Stroke = Colors.LightGray,
                StrokeThickness = 1
            };

            var timePicker = new TimePicker
            {
                Format = "t",
                Margin = new Thickness(10, 5)
            };

            timeBorder.Content = timePicker;
            expandedContent.Add(timeBorder);

            // Área para notas
            expandedContent.Add(new Label
            {
                Text = "Agregar Nota:",
                FontSize = 16,
                Margin = new Thickness(15, 15, 15, 5)
            });

            var notaBorder = new Border
            {
                Margin = new Thickness(15, 0, 15, 10),
                StrokeShape = new Rectangle(),
                Stroke = Colors.LightGray,
                StrokeThickness = 1,
                HeightRequest = 100
            };

            var notaEditor = new Editor
            {
                Placeholder = "Escribe una nota aquí...",
                AutoSize = EditorAutoSizeOption.TextChanges,
                Margin = new Thickness(5)
            };

            notaBorder.Content = notaEditor;
            expandedContent.Add(notaBorder);

            // Botón para guardar
            var guardarButton = new Button
            {
                Text = "Guardar visita",
                BackgroundColor = Color.FromHex("#FFEEAA"),
                TextColor = Colors.Black,
                CornerRadius = 5,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                Margin = new Thickness(0, 0, 0, 20)
            };

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