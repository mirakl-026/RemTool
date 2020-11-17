using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IBackUpService
    {
        public Task SaveServerToZip();

        public Task UnZipToServer();
    }
}
