using SQLite;

namespace RepasoBerny.Mobile.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Contrasena { get; set; }
        public string Tipo { get; set; }
        public string Lada { get; set; }
        public string Numero { get; set; }
    }
}
