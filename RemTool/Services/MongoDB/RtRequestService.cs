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

        public RtRequestService()
        {

        }



        public void CreateRequest(RtRequest rtRequest)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllRequests()
        {
            throw new NotImplementedException();
        }

        public void DeleteRequest(string id)
        {
            throw new NotImplementedException();
        }

        public void MarkRequest(string id, int mark)
        {
            throw new NotImplementedException();
        }

        public void ReadAllRequests()
        {
            throw new NotImplementedException();
        }

        public RtRequest ReadRequest(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequest(RtRequest rtRequest)
        {
            throw new NotImplementedException();
        }
    }
}
