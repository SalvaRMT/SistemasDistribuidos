using PokedexApi.Dtos;
using PokedexApi.Infrastructure.Soap.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Mappers;

public static class HobbiesMapper
{
    public static HobbiesResponse ToDto(this Hobbies hobbies)
    {
        return new HobbiesResponse
        {
            Id = hobbies.Id,
            Name = hobbies.Name,
            Top = hobbies.Top
        };
    }

    public static Hobbies ToModel(this HobbiesResponseDto hobbies)
    {
        return new Hobbies
        {
            Id = hobbies.Id, 
            Name = hobbies.Name,
            Top = hobbies.Top
        };
    }

    public static List<Hobbies> ToModelList(this List<HobbiesResponseDto> hobbies)
    {
        return hobbies?.Select(e => e.ToModel()).ToList() ?? new List<Hobbies>();
    }

    public static List<HobbiesResponse> ToDtoList(this List<Hobbies> hobbies)
    {
        return hobbies?.Select(b => b.ToDto()).ToList() ?? new List<HobbiesResponse>();
    }
}
