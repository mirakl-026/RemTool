using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

using RemTool.Models;
using RemTool.Infrastructure.Additional;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Services.MongoDB
{
    public class RtRequestService : IRtRequestService
    {
        private readonly IMongoCollection<RtRequest> _rtRequests;

        public RtRequestService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _rtRequests = database.GetCollection<RtRequest>("RtRequests");
        }


        #region CRUD
        public void CreateRtRequest(RtRequest rtRequest)
        {
            _rtRequests.InsertOne(rtRequest);
        }

        public RtRequest ReadRtRequest(string id)
        {
            return _rtRequests.Find(rtreq => rtreq.Id == id).FirstOrDefault();
        }

        public void UpdateRtRequest(RtRequest rtRequest)
        {
            _rtRequests.ReplaceOne(rtreq => rtreq.Id == rtRequest.Id, rtRequest);
        }

        public void DeleteRtRequest(string id)
        {
            _rtRequests.DeleteOne(rtreq => rtreq.Id == id);
        }
        #endregion


        public IEnumerable<RtRequest> ReadAllRtRequests()
        {
            return _rtRequests.Find(new BsonDocument()).ToList();
        }

        public void MarkRtRequest(string id, int mark)
        {
            RtRequest rtreq = ReadRtRequest(id);
            if (rtreq != null)
            {
                rtreq.Mark = mark;
                UpdateRtRequest(rtreq);
            }            
        }

        public void DeleteAllRtRequests()
        {
            _rtRequests.DeleteMany(new BsonDocument());
        }




        public async Task CreateRtRequestAsync(RtRequest rtRequest)
        {
            await _rtRequests.InsertOneAsync(rtRequest);
        }

        public async Task<RtRequest> ReadRtRequestAsync(string id)
        {
            return await _rtRequests.Find(rtReq => rtReq.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateRtRequestAsync(RtRequest rtRequest)
        {
            await _rtRequests.ReplaceOneAsync(rtReq => rtReq.Id == rtRequest.Id, rtRequest);
        }

        public async Task DeleteRtRequestAsync(string id)
        {
            await _rtRequests.DeleteOneAsync(rtReq => rtReq.Id == id);
        }
        public async Task<IEnumerable<RtRequest>> ReadAllRtRequestsAsync()
        {
            return await _rtRequests.Find(new BsonDocument()).ToListAsync();
        }

        public async Task DeleteAllRtRequestsAsync()
        {
            await _rtRequests.DeleteManyAsync(new BsonDocument());
        }

        public async Task MarkRtRequestAsync(string id, int mark)
        {
            RtRequest rtreq = await ReadRtRequestAsync(id);
            if (rtreq != null)
            {
                rtreq.Mark = mark;
                await UpdateRtRequestAsync(rtreq);
            }
        }
    }
}
