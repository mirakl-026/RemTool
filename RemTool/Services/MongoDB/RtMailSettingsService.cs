using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Driver;
using RemTool.Infrastructure.Additional;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace RemTool.Services.MongoDB
{
    public class RtMailSettingsService : IRtMailSettingsService
    {
        private readonly IMongoCollection<RtMailSettings> _rtMailSettingsCol;

        public RtMailSettingsService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _rtMailSettingsCol = database.GetCollection<RtMailSettings>("RtMailSettings");
        }



        #region CRUD_sync
        public void CreateRtMailSettings(RtMailSettings mailSettings)
        {
            _rtMailSettingsCol.InsertOne(mailSettings);
        }

        public RtMailSettings ReadRtMailSettings()
        {
            return _rtMailSettingsCol.Find(new BsonDocument()).FirstOrDefault();
        }

        public void UpdateRtMailSettings(RtMailSettings newRtMailSettings)
        {
            DeleteRtMailSettings();
            CreateRtMailSettings(newRtMailSettings);
        }

        public void UpdateRtMailSettingsFromCurrent(RtMailSettings refreshedRtMailSettings)
        {
            _rtMailSettingsCol.ReplaceOne(ms => ms.Id == refreshedRtMailSettings.Id, refreshedRtMailSettings);
        }


        public void DeleteRtMailSettings()
        {
            _rtMailSettingsCol.DeleteMany(new BsonDocument());
        }
        #endregion


        public void ChangeFlag_notificationToHQ(bool value)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (currentRtMailSettings.SendNotificationToHQ != value)
            {
                currentRtMailSettings.SendNotificationToHQ = value;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeHQeMail(string eMail)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.HQeMail.Equals(eMail))
            {
                currentRtMailSettings.HQeMail = eMail;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeFlag_notificationToClient(bool value)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (currentRtMailSettings.SendNotificationToClient != value)
            {
                currentRtMailSettings.SendNotificationToClient = value;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeDefaultMessageToClient(string message)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.DefaultMessageToClient.Equals(message))
            {
                currentRtMailSettings.DefaultMessageToClient = message;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }


        public void ChangeCredentials_Name(string credentialsName)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.Credentials_Name.Equals(credentialsName))
            {
                currentRtMailSettings.Credentials_Name = credentialsName;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeCredentials_Pass(string credentialsPass)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.Credentials_Pass.Equals(credentialsPass))
            {
                currentRtMailSettings.Credentials_Pass = credentialsPass;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeSmtpServer_Host(string smtp_host)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.SmtpServer_Host.Equals(smtp_host))
            {
                currentRtMailSettings.SmtpServer_Host = smtp_host;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }

        public void ChangeSmtpServer_Port(string smtp_port)
        {
            var currentRtMailSettings = ReadRtMailSettings();

            if (!currentRtMailSettings.SmtpServer_Port.Equals(smtp_port))
            {
                currentRtMailSettings.SmtpServer_Port = smtp_port;
                UpdateRtMailSettingsFromCurrent(currentRtMailSettings);
            }
        }
    }
}
