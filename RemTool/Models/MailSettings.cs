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
        public bool SendNotificationToHQ { get; set; }

        // почта админа для оповещений
        public string HQeMail { get; set; }

        // флаг об отправке на почту клиенту
        public bool SendNotificationToClient { get; set; }

        // сообщение по умолчанию в письме запросящему
        public string DefaultMessageToClient { get; set; }


        // почта за счёт которой идёт отправка
        public string sender_eMail { get; set; }

        public string sender_Pass { get; set; }
    }
}
