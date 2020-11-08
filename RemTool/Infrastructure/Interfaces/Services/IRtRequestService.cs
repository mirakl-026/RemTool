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
        // синхроннные методы 
        #region CRUD
        public void CreateRtRequest(RtRequest rtRequest);

        public RtRequest ReadRtRequest(string id);

        public void UpdateRtRequest(RtRequest rtRequest);

        public void DeleteRtRequest(string id);
        #endregion

        public void MarkRtRequest(string id, int mark);

        public IEnumerable<RtRequest> ReadAllRtRequests();

        public void DeleteAllRtRequests();


        // асинхронные методы
        #region CRUD_Async
        public Task CreateRtRequestAsync(RtRequest rtRequest);

        public Task<RtRequest> ReadRtRequestAsync(string id);

        public Task UpdateRtRequestAsync(RtRequest rtRequest);

        public Task DeleteRtRequestAsync(string id);
        #endregion

        public Task MarkRtRequestAsync(string id, int mark);

        public Task<IEnumerable<RtRequest>> ReadAllRtRequestsAsync();

        public Task DeleteAllRtRequestsAsync();
    }
}
