using PaisesApp.Models;

namespace PaisesApp.Services;

public interface IFavoritosRepository
{
    Task<List<Favorito>> ObtenerTodosAsync();
    Task GuardarAsync(Favorito favorito);
    Task EliminarAsync(Favorito favorito);
}
