namespace PokemonApi.Dtos;

public class PokemonResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }  // <- Asegurar que este campo existe
    public StatsDto Stats { get; set; }

    // Constructor corregido con parámetros obligatorios
    public PokemonResponseDto(Guid id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}

public class StatsDto
{
    public int Attack { get; internal set; }
    public int Speed { get; internal set; }
    public object Desense { get; internal set; }
}