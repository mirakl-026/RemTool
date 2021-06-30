using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;


namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private IMetaDataService db;

        public MetaDataController (IMetaDataService context)
        {
            db = context;
        }

        // GET api/MetaData/GetMetaData
        [HttpGet("GetMetaData")]
        public async Task<MetaData> GetMetaData()
        {
            return await db.ReadMetaDataAsync();
        }


        // GET api/MetaData/GetPhoneNumber
        [HttpGet("GetPhoneNumber")]
        public async Task<string> GetPhoneNumber()
        {
            MetaData md = await db.ReadMetaDataAsync();
            return md.PhoneNumber;
        }

        // POST /api/metadata/setphonenumber?number=124
        [HttpPost("SetPhoneNumber")]
        public void SetPhoneNumber(string number)
        {
            MetaData md = db.ReadMetaData();
            md.PhoneNumber = number;
            db.UpdateMetaData(md);
        }
    }
}
