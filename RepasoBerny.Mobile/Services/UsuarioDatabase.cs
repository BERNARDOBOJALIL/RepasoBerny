using SQLite;
using RepasoBerny.Mobile.Models;

namespace RepasoBerny.Mobile.Services
{
    public class UsuarioDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UsuarioDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            return _database.InsertAsync(usuario);
        }

        public async Task<int> ObtenerCantidadUsuariosAsync()
        {
            return await _database.Table<Usuario>().CountAsync();
        }
    }
}
