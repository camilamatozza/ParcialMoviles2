using PaisesApp.Models;
using SQLite;

using PaisesApp.Models;
using SQLite;

namespace PaisesApp.Services;

public class FavoritosRepository : IFavoritosRepository
{
    private readonly SQLiteAsyncConnection _db;

    public FavoritosRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "favoritos.db3");
        _db = new SQLiteAsyncConnection(path);
        _db.CreateTableAsync<Favorito>().Wait();
    }

    public Task<List<Favorito>> ObtenerTodosAsync() =>
        _db.Table<Favorito>().ToListAsync();

    public Task GuardarAsync(Favorito favorito) =>
        _db.InsertAsync(favorito);

    public Task EliminarAsync(Favorito favorito) =>
        _db.DeleteAsync(favorito);
}