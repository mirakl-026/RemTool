using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Models
{
    // инструмент
    public class Tool
    {
        public int Id { get; set; }

        public int MainType { get; set; }

        public int SecondaryType { get; set; }

        public Brand Brand { get; set; }

        public string Specs { get; set; }

        public string Description { get; set; }

        public string ImgReference { get; set; }
    }
}
