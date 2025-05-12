using PokedexApi.Dtos;
using PokedexApi.Infrastructure.Soap.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Mappers;

public static class HobbyMapper
{
    public static HobbyResponse ToDto(this Hobby hobby){
        return new HobbyResponse{
            Id = hobby.Id,
            Name = hobby.Name,
            Top = hobby.Top
        };
    }

    public static Hobby ToModel(this HobbyResponseDto hobby){
        return new Hobby{
            Id = hobby.Id,
            Name = hobby.Name,
            Top = hobby.Top
        };
    }

    public static List<Hobby> ToModelList(this List<HobbyResponseDto> hobby)
    {
        return hobby?.Select(e => e.ToModel()).ToList() ?? new List<Hobby>();
    }
    public static List<HobbyResponse> ToDtoList(this List<Hobby> hobby)
    {
        return hobby?.Select(b => b.ToDto()).ToList() ?? new List<HobbyResponse>();
    }
}
