using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Модель заявки
namespace RemTool.Models
{
    public class RtRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ReqInfo { get; set; }

        // пометка - выполнено, прочитано, под вопросом и т.д.
        public int Mark { get; set; }
    }
}

