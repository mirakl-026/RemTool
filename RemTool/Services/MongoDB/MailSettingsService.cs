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
    public class MailSettingsService : IMailSettingsService
    {
        private readonly IMongoCollection<MailSettings> _mailSettingsCol;

        public MailSettingsService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mailSettingsCol = database.GetCollection<MailSettings>("MailSettings");
        }



        #region CRUD_sync
        public void CreateMailSettings(MailSettings mailSettings)
        {
            _mailSettingsCol.InsertOne(mailSettings);
        }

        public MailSettings ReadMailSettings()
        {
            return _mailSettingsCol.Find(new BsonDocument()).FirstOrDefault();
        }

        public void UpdateMailSettings(MailSettings newMailSettings)
        {
            DeleteMailSettings();
            CreateMailSettings(newMailSettings);
        }

        public void UpdateMailSettingsFromCurrent(MailSettings refreshedMailSettings)
        {
            _mailSettingsCol.ReplaceOne(ms => ms.Id == refreshedMailSettings.Id, refreshedMailSettings);
        }


        public void DeleteMailSettings()
        {
            _mailSettingsCol.DeleteMany(new BsonDocument());
        }
        #endregion


        public void ChangeFlag_notificationToHQ(bool value)
        {
            var currentMailSettings = ReadMailSettings();

            if (currentMailSettings.SendNotificationToHQ != value)
            {
                currentMailSettings.SendNotificationToHQ = value;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }

        public void ChangeHQeMail(string eMail)
        {
            var currentMailSettings = ReadMailSettings();

            if (!currentMailSettings.HQeMail.Equals(eMail))
            {
                currentMailSettings.HQeMail = eMail;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }

        public void ChangeFlag_notificationToClient(bool value)
        {
            var currentMailSettings = ReadMailSettings();

            if (currentMailSettings.SendNotificationToClient != value)
            {
                currentMailSettings.SendNotificationToClient = value;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }

        public void ChangeDefaultMessageToClient(string message)
        {
            var currentMailSettings = ReadMailSettings();

            if (!currentMailSettings.DefaultMessageToClient.Equals(message))
            {
                currentMailSettings.DefaultMessageToClient = message;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }


        public void ChangeSender_eMail(string eMail)
        {
            var currentMailSettings = ReadMailSettings();

            if (!currentMailSettings.sender_eMail.Equals(eMail))
            {
                currentMailSettings.sender_eMail = eMail;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }

        public void ChangeSender_pS(string pS)
        {
            var currentMailSettings = ReadMailSettings();

            if (!currentMailSettings.sender_Pass.Equals(pS))
            {
                currentMailSettings.sender_Pass = pS;
                UpdateMailSettingsFromCurrent(currentMailSettings);
            }
        }
    }
}
