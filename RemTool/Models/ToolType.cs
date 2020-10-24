using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemTool.Models
{
    public class ToolType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        // основной из 8-и типов
        public int MainType { get; set; }

        // второстепенный из основного 
        public int SecondaryType { get; set; }

        // бренды которые включены в тип-инструмента
        public List<string> Brands { get; set; }

        // список услуг - название услуги - значение в деньгах 
        public Dictionary<string, string> ServeCost { get; set; } 

        // ссылка на картинку (при расширении)
        public string ImgRefenrence { get; set; }
    }
}
