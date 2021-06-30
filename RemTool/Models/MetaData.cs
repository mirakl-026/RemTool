using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


// общие мета данные - типа номера телефона , ссылки на карты и т.д.
namespace RemTool.Models
{
    public class MetaData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // флаг об отправке оповещений на почту админа
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
