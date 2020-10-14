using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemTool.Models
{
    // инструмент
    public class Tool
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public int MainType { get; set; }

        public int SecondaryType { get; set; }

        public string BrandName { get; set; }

        public string Specs { get; set; }

        public string Description { get; set; }

        public string ImgReference { get; set; }
    }
}
