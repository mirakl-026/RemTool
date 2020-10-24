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

        public int MainType { get; set; }

        public int SecondaryType { get; set; }

        public List<string> Brands { get; set; }

        public Dictionary<string, int> ServeCost { get; set; }        

        public string ImgReference { get; set; }
    }
}
