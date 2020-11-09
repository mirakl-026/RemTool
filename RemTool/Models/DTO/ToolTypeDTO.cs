﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

namespace RemTool.Models.DTO
{
    public class ToolTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MainType { get; set; }
        public int SecondaryType { get; set; }
        public string[] Brands { get; set; }
        public SC_Dictionary ServeCost { get; set; }
        public string ImgRefenrence { get; set; }
    }

    public class SC_Dictionary
    {
        public string[] Keys { get; set; }

        public string[] Values { get; set; }
    }
}
