using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemTool.Models
{
    public class MailSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // флаг об отправке оповещений на почту админа
        public bool SendMailToHQ { get; set; }

        // почта админа для оповещений
        public string HQ_mail { get; set; }

        // флаг об отправке на почту запросящего
        public bool SendMailToRequester { get; set; }

        // сообщение по умолчанию в письме запросящему
        public string DefaultMessageToRequester { get; set; }


        // почта за счёт которой идёт отправка
        public string senderMail { get; set; }

        public string senderPass { get; set; }
    }
}
