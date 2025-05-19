using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainerApi.Infrastructure.Documents;
using TrainerApi.Models;
using TrainerApi.Infrastructure;
using TrainerApi.Mappers;
using System.Xml.Linq;

namespace TrainerApi.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly IMongoCollection<TrainerDocument> _TrainersCollection;

    public TrainerRepository(IMongoDatabase database, IOptions<MongoDBSettings> settings){
       _TrainersCollection = database.GetCollection<TrainerDocument>(settings.Value.TrainersCollectionName);
    }

    public async Task<Trainer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var trainer = await _TrainersCollection.Find(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
        return trainer?.ToModel();
    }

    public async Task<Trainer> CreateAsync(Trainer trainer, CancellationToken cancellationToken){
        var document = trainer.ToDocument();
        await _TrainersCollection.InsertOneAsync(document, cancellationToken);
        return document.ToModel();
    }

    public async Task<List<Trainer>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var filter = Builders<TrainerDocument>.Filter.Regex(
                t => t.Name,
                new MongoDB.Bson.BsonRegularExpression(name, "i")
            );
            var docs = await _TrainersCollection
                .Find(filter)
                .ToListAsync(cancellationToken);

            return docs
                .Select(d => d.ToModel()!)
                .ToList();
        }

    public Task<List<Trainer>> GetByNameAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


    /*public async Task<List<Trainer>> GetByNameAsync(CancellationToken cancellationToken)
    {
        var trainers = await _TrainersCollection.Find(s => s.Name.Contains(name)).ToListAsync(cancellationToken);
        return trainers.Select(s => s.ToModel()).ToList();
    }

    public Task<List<Trainer>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }*/
