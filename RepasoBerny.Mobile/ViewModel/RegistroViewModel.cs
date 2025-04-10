using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepasoBerny.Mobile.ViewModel
{
    public partial class RegistroViewModel : ObservableValidator
    {
        public ObservableCollection<string> Errors { get; set; } = new();

        // Nombre
        private string nombre;
        public string NombreError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value, true);
        }

        // Apellidos
        private string apellidos;
        public string ApellidosError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        public string Apellidos
        {
            get => apellidos;
            set => SetProperty(ref apellidos, value, true);
        }

        // Contraseña
        private string contrasena;
        public string ContrasenaError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        [DataType(DataType.Password)]
        public string Contrasena
        {
            get => contrasena;
            set => SetProperty(ref contrasena, value, true);
        }

        // Confirmar Contraseña
        private string confirmarcontrasena;
        public string ConfirmarContrasenaError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        [DataType(DataType.Password)]
        public string Confirmarcontrasena
        {
            get => confirmarcontrasena;
            set => SetProperty(ref confirmarcontrasena, value, true);
        }

        // Tipo
        private string tipo;
        public string TipoError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Tipo
        {
            get => tipo;
            set => SetProperty(ref tipo, value, true);
        }

        public ObservableCollection<string> Tipos { get; } = new()
        {
            "Cliente Bancoppel",
            "Credito Coppel",
            "Afore Bancoppe",
            "No soy cliente"
        };

        // Lada
        private string lada;
        public string LadaError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string Lada
        {
            get => lada;
            set => SetProperty(ref lada, value, true);
        }

        // Número
        private string numero;
        public string NumeroError { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo debe tener máximo {1} caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string Numero
        {
            get => numero;
            set => SetProperty(ref numero, value, true);
        }

        // Error general combinado
        public string ErroresUnidos => string.Join("\n", Errors);

        [RelayCommand]
        public async Task RegistrarColaborador()
        {
            ValidateAllProperties();
            Errors.Clear();

            NombreError = GetErrors(nameof(Nombre)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            ApellidosError = GetErrors(nameof(Apellidos)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            ContrasenaError = GetErrors(nameof(Contrasena)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            ConfirmarContrasenaError = GetErrors(nameof(Confirmarcontrasena)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            TipoError = GetErrors(nameof(Tipo)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            LadaError = GetErrors(nameof(Lada)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
            NumeroError = GetErrors(nameof(Numero)).FirstOrDefault()?.ErrorMessage ?? string.Empty;

            // Agregar errores a la lista general (opcional si solo usas los individuales)
            if (!string.IsNullOrEmpty(NombreError)) Errors.Add(NombreError);
            if (!string.IsNullOrEmpty(ApellidosError)) Errors.Add(ApellidosError);
            if (!string.IsNullOrEmpty(ContrasenaError)) Errors.Add(ContrasenaError);
            if (!string.IsNullOrEmpty(ConfirmarContrasenaError)) Errors.Add(ConfirmarContrasenaError);
            if (!string.IsNullOrEmpty(TipoError)) Errors.Add(TipoError);
            if (!string.IsNullOrEmpty(LadaError)) Errors.Add(LadaError);
            if (!string.IsNullOrEmpty(NumeroError)) Errors.Add(NumeroError);

            // Notificar para refrescar los labels en XAML
            OnPropertyChanged(nameof(NombreError));
            OnPropertyChanged(nameof(ApellidosError));
            OnPropertyChanged(nameof(ContrasenaError));
            OnPropertyChanged(nameof(ConfirmarContrasenaError));
            OnPropertyChanged(nameof(TipoError));
            OnPropertyChanged(nameof(LadaError));
            OnPropertyChanged(nameof(NumeroError));
            OnPropertyChanged(nameof(ErroresUnidos));

            if (!HasErrors)
            {
                var nuevoUsuario = new Models.Usuario
                {
                    Nombre = this.Nombre,
                    Apellidos = this.Apellidos,
                    Contrasena = this.Contrasena,
                    Tipo = this.Tipo,
                    Lada = this.Lada,
                    Numero = this.Numero
                };

                await App.Database.SaveUsuarioAsync(nuevoUsuario);

                await Shell.Current.DisplayAlert("Éxito", "Registro guardado localmente.", "OK");

                // Clear form if you want
                Nombre = Apellidos = Contrasena = Confirmarcontrasena = Tipo = Lada = Numero = string.Empty;
                Errors.Clear();
            }
        }

        [RelayCommand]
        public async Task MostrarCantidadUsuarios()
        {
            int cantidad = await App.Database.ObtenerCantidadUsuariosAsync();

            await Shell.Current.DisplayAlert("Base de datos",
                $"Hay {cantidad} usuario(s) registrado(s).", "OK");
        }
    }
}
