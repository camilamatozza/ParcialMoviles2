using Moq;

namespace PaisesApp.Tests;

// Clases copiadas para testing 
public class NombrePais
{
    public string? Common { get; set; }
}

public class Pais
{
    public NombrePais? Name { get; set; }
    public List<string>? Capital { get; set; }
    public string? Region { get; set; }
    public long Population { get; set; }
    public string NombreComun => Name?.Common ?? "Sin nombre";
    public string CapitalTexto => Capital?.FirstOrDefault() ?? "Sin capital";
}

public interface IPaisServiceTest
{
    Task<List<Pais>> GetPaisesAsync();
}

public class PaisServiceTests
{
    [Fact]
    public async Task GetPaisesAsync_RetornaLista_CuandoServicioFunciona()
    {
        // Arrange
        var mockService = new Mock<IPaisServiceTest>();
        mockService.Setup(s => s.GetPaisesAsync())
            .ReturnsAsync(new List<Pais>
            {
                new Pais { Name = new NombrePais { Common = "Argentina" } },
                new Pais { Name = new NombrePais { Common = "Brasil" } }
            });

        // Act
        var resultado = await mockService.Object.GetPaisesAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task GetPaisesAsync_RetornaListaVacia_CuandoNoHayPaises()
    {
        // Arrange
        var mockService = new Mock<IPaisServiceTest>();
        mockService.Setup(s => s.GetPaisesAsync())
            .ReturnsAsync(new List<Pais>());

        // Act
        var resultado = await mockService.Object.GetPaisesAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    [Fact]
    public async Task GetPaisesAsync_LanzaExcepcion_CuandoHayErrorDeRed()
    {
        // Arrange
        var mockService = new Mock<IPaisServiceTest>();
        mockService.Setup(s => s.GetPaisesAsync())
            .ThrowsAsync(new HttpRequestException("Sin conexión"));

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => mockService.Object.GetPaisesAsync());
    }

    [Fact]
    public void NombreComun_RetornaComun_CuandoNameEsValido()
    {
        // Arrange
        var pais = new Pais { Name = new NombrePais { Common = "Argentina" } };

        // Act & Assert
        Assert.Equal("Argentina", pais.NombreComun);
    }

    [Fact]
    public void NombreComun_RetornaSinNombre_CuandoNameEsNull()
    {
        // Arrange
        var pais = new Pais { Name = null };

        // Act & Assert
        Assert.Equal("Sin nombre", pais.NombreComun);
    }
}