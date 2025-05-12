using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Exceptions;
using Microsoft.AspNetCore.Authorization;
namespace PokedexApi.Controllers;
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    public PokemonsController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    //localhost/api/v1/pokemons/123524-1234
    [HttpGet("{id}")]
    [Authorize(Policy = "Read")]
    public async Task<ActionResult<PokemonResponse>> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonById(id, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDto());
    }

    [HttpGet("by-name/{name}")]
    //localhost/api/v1/pokemons/NAME ---esto no esta bien
    //localhost/api/v1/pokemons/?name=NOMBRE ---esto esta bien, el "?" indica que son query parameters
    //---mala practica---
   /* [HttpGet("name/{name}")]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDtoList());
    }*/

    //---Buena practica---
    [HttpGet]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonByName([FromQuery]string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDtoList());
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Write")]
    //204 - NoContent (Se encontro y se elimino el pokemon de manera correcta pero el body de respuesta esta vacio)
    //200 - ok (se encontro y se elimino y en el body de respuesta se manda un mensaje de exito)
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if (deleted){
            return NoContent(); //204
        }
        return NotFound(); //404
    }
    //400 - badrequest (usuario ingreso un valor incorrecto)
    //409 - conflict (ya existe el resucrso que se quiere crear)
    //200 - ok (objeto de respuesta pokemon creado)
    //201 - created (pokemon creado, en headres de respuesta url de recurso creado)
[HttpPost]
public async Task<ActionResult<PokemonResponse>> CreatePokemonRequest(
    [FromBody] CreatePokemonRequest Pokemon, CancellationToken cancellationToken)
{
    if (Pokemon == null)
    {
        return BadRequest(new { message = "Invalid request data." });
    }

    try
    {
        var createdPokemon = await _pokemonService.CreatePokemonAsync(Pokemon.ToModel(), cancellationToken);
        return CreatedAtAction(nameof(GetPokemonById), new { id = createdPokemon.Id }, createdPokemon.ToDto());
    }
    catch (PokemonAlreadyExistsException ex) 
    {
        return Conflict(new { message = ex.Message }); // 409 Conflict
    }
    catch (PokemonValidationException ex)
    {
        return BadRequest(new { message = ex.Message }); // ✅ Devuelve el 400 correcto
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
    }
}
//UPDATE POKEMON
//PUT - LOCALHOST:PORT/API/V1/Pokemons/ID
//Request body (name, type, level, stats...)
//200 - ok (se encontro y se actualizo el pokemon de manera correcta)
//404 - not found (No existe el pokemon con el id que se manda)
//400 - badrequest (usuario ingreso un valor incorrecto)
//409 - conflict (ya existe el resucrso que se quiere crear)
//204 - NoContent 
//200 - ok (Pokemon actualizado)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePokemon(Guid id,[FromBody] UpdatePokemonRequest Pokemon, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonService.UpdatePokemonAsync(id, Pokemon.ToModel(), cancellationToken);
            return NoContent();
        }
        catch (PokemonConflictException){
            return Conflict(new { message =$"Pokemon already exists with the name: {Pokemon.Name}"});
        }
        catch (PokemonValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch(PokemonNotFoundException)
        {
return NotFound();
        }
    }  

//PATCH - localhost:port/api/v1/pokemon/IDD
//Request body (level:10)
//[HttpPatch("{id}")]

}