package service

type PokemonService interface {
	GetPokemonByID(id string) (model.Pokemon, error)
}

type pokemonService struct {
}

func NewPokemonService() PokemonService {
	return &pokemonService{}
}

func (s *pokemonService) GetPokemonByID(id string) (model.Pokemon, error) {

}
