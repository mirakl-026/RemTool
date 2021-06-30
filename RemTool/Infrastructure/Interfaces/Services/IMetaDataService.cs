using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IMetaDataService
    {
        #region CRUD_sync

        public void CreateMetaData(MetaData md);

        public MetaData ReadMetaData();

        public void UpdateMetaData(MetaData newMd);

        public void DeleteMetaData();

        #endregion

        public Task<MetaData> ReadMetaDataAsync();
    }
}
