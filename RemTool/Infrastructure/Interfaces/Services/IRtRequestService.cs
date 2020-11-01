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

        public void CreateRtRequest(RtRequest rtRequest);

        public RtRequest ReadRtRequest(string id);

        public void UpdateRtRequest(RtRequest rtRequest);

        public void DeleteRtRequest(string id);

        #endregion

        public void MarkRtRequest(string id, int mark);

        public IEnumerable<RtRequest> ReadAllRtRequests();

        public void DeleteAllRtRequests();
    }
}
