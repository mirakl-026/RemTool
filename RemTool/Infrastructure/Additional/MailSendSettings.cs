using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Infrastructure.Additional
{
    public class MailSendSettings : IMailSendSettings
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string Credentials_Name { get; set; }

        public string Credentials_Pass { get; set; }

        public string EnableSsl { get; set; }
    }

    public interface IMailSendSettings
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string Credentials_Name { get; set; }

        public string Credentials_Pass { get; set; }

        public string EnableSsl { get; set; }
    }
}
