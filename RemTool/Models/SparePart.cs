using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemTool.Models
{
    // запчасть
    public class SparePart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string Specs { get; set; }

        public string Description { get; set; }

        public string ImgReference { get; set; }

    }
}
