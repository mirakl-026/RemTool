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
    }
}
