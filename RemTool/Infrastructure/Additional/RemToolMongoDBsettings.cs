using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Infrastructure.Additional
{
    public class RemToolMongoDBsettings : IRemToolMongoDBsettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IRemToolMongoDBsettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
