
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainerApi.Infrastructure.Documents;

public class TrainerDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("age")]
    public int Age { get; set; }
    [BsonElement("birthdate")]
    public DateTime Birthdate { get; set; }
    [BsonElement("create_at")]
    public DateTime CreatedAt { get; set; }
    [BsonElement("medals")]
    public List<MedalDocument> Medals { get; set; }

}