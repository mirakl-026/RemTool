using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IMailSettingsService
    {
        // настройки mail существуют в единственном экземпляре, поэтому не нужно беспокоится об id
        #region CRUD_sync

        public void CreateMailSettings(MailSettings mailSettings);

        public MailSettings ReadMailSettings();

        public void UpdateMailSettings(MailSettings newMailSettings);

        public void UpdateMailSettingsFromCurrent(MailSettings refreshedMailSettings);

        public void DeleteMailSettings();

        #endregion

        public void ChangeHQeMail(string eMail);

        public void ChangeFlag_notificationToHQ(bool value);

        public void ChangeFlag_notificationToClient(bool value);

        public void ChangeDefaultMessageToClient(string message);

        public void ChangeCredentials_Name(string credentialsName);

        public void ChangeCredentials_Pass(string credentialsPass);

        public void ChangeSmtpServer_Host(string smtp_host);

        public void ChangeSmtpServer_Port(string smtp_port);
    }
}
