using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemTool.Models
{
    public class ToolTypeSearch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name {get; set;}

        public string RefId { get; set; }

        // массив ключевых слов для поиска
        public string[] KeyWords{ get; set; } 
    }  
}
