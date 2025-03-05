using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using PokemonApi.Dto;
using PokemonApi.Mappers;
using PokemonApi.Models;
using PokemonApi.Repositories;

namespace PokemonApi.Services
{
    public class HobbiesService : IHobbiesService
    {
        private readonly IHobbiesRepository _hobbiesRepository;

        public HobbiesService(IHobbiesRepository hobbiesRepository)
        {
            _hobbiesRepository = hobbiesRepository;
        }

        public async Task<HobbiesResponseDto> GetHobbiesById(int id, CancellationToken cancellationToken)
        {
            var hobbies = await _hobbiesRepository.GetHobbyById(id, cancellationToken);
            if (hobbies == null)
            {
                throw new FaultException("Hobbies not found :(");
            }
            return hobbies.ToDto();
        }

        public async Task<bool> DeleteHobbies(int id, CancellationToken cancellationToken)
        {
            var hobbies = await _hobbiesRepository.GetHobbyById(id, cancellationToken);
            if (hobbies == null)
            {
                throw new FaultException("Hobbies not found :(");
            }
            await _hobbiesRepository.DeleteHobby(hobbies, cancellationToken);
            return true;
        }

        public async Task<List<HobbiesResponseDto>> GetHobbieByName(string name, CancellationToken cancellationToken)
        {
            var hobbies = await _hobbiesRepository.GetHobbiesByName(name, cancellationToken);
            if (hobbies == null || !hobbies.Any())
            {
                return new List<HobbiesResponseDto>();
            }
            return hobbies.Select(h => h.ToDto()).ToList();
        }

        public async Task<HobbiesResponseDto> CreateHobbies(CreateHobbiesDto createHobbiesDto, CancellationToken cancellationToken)
        {
            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(createHobbiesDto.Name))
            {
                throw new FaultException("El nombre del hobby es requerido");
            }

            // Convertir el DTO en el modelo
            var hobby = new Hobbies
            {
                Name = createHobbiesDto.Name,
                Top = createHobbiesDto.Top
            };

            await _hobbiesRepository.AddHobbyAsync(hobby, cancellationToken);
            return hobby.ToDto();
        }

        public async Task<HobbiesResponseDto> UpdateHobbies(UpdateHobbiesDto updateHobbiesDto, CancellationToken cancellationToken)
        {
            var existingHobby = await _hobbiesRepository.GetHobbyById(updateHobbiesDto.Id, cancellationToken);
            if (existingHobby == null)
            {
                throw new FaultException("Hobby not found");
            }

            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(updateHobbiesDto.Name))
            {
                throw new FaultException("El nombre del hobby es requerido");
            }

            // Actualizar el modelo
            existingHobby.Name = updateHobbiesDto.Name;
            existingHobby.Top = updateHobbiesDto.Top;

            await _hobbiesRepository.UpdateHobbyAsync(existingHobby, cancellationToken);
            return existingHobby.ToDto();
        }
    }
}
