using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Models
{
    // запчасть
    public class SparePart
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }

        public string Specs { get; set; }

        public string Description { get; set; }

        public string ImgReference { get; set; }

    }
}
