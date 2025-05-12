namespace PokedexApi.Exceptions;

    public class PokemonAlreadyExistsException : Exception{
        public PokemonAlreadyExistsException(string message) : base(message){
            
        }
    }
