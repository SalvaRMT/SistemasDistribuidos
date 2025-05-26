using Google.Protobuf.WellKnownTypes;
using TrainerApi.Infrastructure.Documents; 
using TrainerApi.Models; 

namespace TrainerApi.Mappers;

public static class TrainerMapper
{

    public static Trainer? ToModel(this TrainerDocument trainerDocument)
    {

        if (trainerDocument == null)
            return null;

        return new Trainer
        {
            Id = trainerDocument.Id,
            Name = trainerDocument.Name,
            Age = trainerDocument.Age,
            Birthdate = trainerDocument.Birthdate,
            CreatedAt = trainerDocument.CreatedAt,
            Medals = trainerDocument.Medals.Select(s => new Medal
            {
                Region = s.Region, 
                Type = (Models.MedalType)(int)s.Type 
            }).ToList()
        };
    }

    public static Trainer ToModel(this CreateTrainerRequest trainer){
        return new Trainer{
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate.ToDateTime(),
            CreatedAt = DateTime.UtcNow,
            Medals = trainer.Medals.Select(s => new Medal{
                Region = s.Region,
                Type = (Models.MedalType)(int)s.Type
            }).ToList()
        };
    }

    public static TrainerDocument ToDocument(this Trainer trainer)
    {
        return new TrainerDocument
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt,
            Medals = trainer.Medals.Select(s => new MedalDocument{
                Region = s.Region,
                Type = (TrainerApi.Infrastructure.Documents.MedalType)(int)s.Type
            }).ToList()
        };
    }

        public static TrainerResponse ToResponse(this Trainer trainer)
        {
            return new TrainerResponse
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Age = trainer.Age,
                Birthdate = Timestamp.FromDateTime(trainer.Birthdate.ToUniversalTime()),
                CreatedAt = Timestamp.FromDateTime(trainer.CreatedAt.ToUniversalTime()),
                Medals =
                {
                    trainer.Medals.Select(s => new TrainerApi.Medals
                    {
                        Region = s.Region,
                        Type = (MedalType)(int)s.Type
                    })
                }
            };
        }
    /*
    
    */
}