using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TrainerApi;
using TrainerApi.Repositories;
using TrainerApi.Mappers;
using Microsoft.AspNetCore.WebUtilities;

namespace TrainerApi.Services;

public class TrainerService : TrainerApi.TrainerService.TrainerServiceBase
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }
    public override async Task<TrainerResponse> GetTrainer(TrainerByIdRequest request, ServerCallContext context)
    {
        var trainer = await _trainerRepository.GetByIdAsync(request.Id, context.CancellationToken);
        if (trainer is null){
            throw new RpcException(new Status(StatusCode.NotFound, "Trainer not found"));
        }
        return trainer.ToResponse();
    }
    
    public override async Task<CreateTrainersResponse> CreateTrainer(IAsyncStreamReader<CreateTrainerRequest> requestStream, ServerCallContext context)
    {
        var createdTrainers = new List<TrainerResponse>();
        while(await requestStream.MoveNext(context.CancellationToken)){
            var request = requestStream.Current;
            var trainer = request.ToModel();
            var trainerExists = await _trainerRepository.GetByNameAsync(trainer.Name, context.CancellationToken);
            if (trainerExists.Any()){
                continue;
            }
            var createdTrainer = await _trainerRepository.CreateAsync(trainer, context.CancellationToken);
            createdTrainers.Add(createdTrainer.ToResponse());
        }
        return new CreateTrainersResponse{
            SuccessCount = createdTrainers.Count,
            Trainers = {createdTrainers}
        };
    }
}

/*return new TrainerResponse {
            Id = Guid.NewGuid().ToString(),
            Name = "Salvador Rojas",
            Age = 21,
            Birthdate = Timestamp.FromDateTime(DateTime.UtcNow),
            CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow),
            Medals = 
            { 
                new Medals { Region = "MX", Type = MedalType.Gold},
                new Medals { Region = "MX", Type = MedalType.Silver}
            }*/