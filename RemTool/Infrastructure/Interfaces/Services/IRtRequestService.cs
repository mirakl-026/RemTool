using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

// интерфейс сервиса управления запросами от пользователей
namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IRtRequestService
    {

        #region CRUD

        public void CreateRequest(RtRequest rtRequest);

        public RtRequest ReadRequest(string id);

        public void UpdateRequest(RtRequest rtRequest);

        public void DeleteRequest(string id);

        #endregion

        public void MarkRequest(string id, int mark);

        public void ReadAllRequests();

        public void DeleteAllRequests();
    }
}
