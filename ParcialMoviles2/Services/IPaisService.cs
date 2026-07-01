using PaisesApp.Models;

namespace PaisesApp.Services;

public interface IPaisService
{
    Task<List<Pais>> GetPaisesAsync();
}