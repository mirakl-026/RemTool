using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// модель счётчика по инструменту
namespace RemTool.Models
{
    public class ClickCounter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ToolTypeId { get; set; }

        public int Count { get; set; }
    }
}
