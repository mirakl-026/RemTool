using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Models
{
    // Бренд
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgReference { get; set; }

        public string HomePage { get; set; }
    }
}
