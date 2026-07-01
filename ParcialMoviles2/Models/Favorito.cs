using SQLite;

namespace PaisesApp.Models;

public class Favorito
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string NombrePais { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
}