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

        public IEnumerable<RtRequest> ReadAllRtRequests();

        public void DeleteAllRtRequests();

        public RtRequest ReadRtRequestByPhone(string phone);


        // асинхронные методы
        #region CRUD_Async
        public Task CreateRtRequestAsync(RtRequest rtRequest);

        public Task<RtRequest> ReadRtRequestAsync(string id);

        public Task UpdateRtRequestAsync(RtRequest rtRequest);

        public Task DeleteRtRequestAsync(string id);
        #endregion

        public Task<IEnumerable<RtRequest>> ReadAllRtRequestsAsync();

        public Task DeleteAllRtRequestsAsync();

        public Task<RtRequest> ReadRtRequestByPhoneAsync(string phone);
    }
}
