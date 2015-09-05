using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    public class ClientMessage
    {
        public string OwnerGuid { get; private set; }
        public string OwnerAccount { get; private set; }
        public string OwnerAvatar { get; private set; }
        public string OwnerSex { get; private set; }
        public string Guid { get; private set; }
        public string Cont { get; private set; }
        public DateTime CreateTime { get; private set; }

        public ClientMessage(string ownerGuid, string ownerAccount, string ownerAvatar, string ownerSex, string guid, string cont, DateTime createTime)
        {
            OwnerGuid = ownerGuid;
            OwnerAccount = ownerAccount;
            OwnerAvatar = ownerAvatar;
            OwnerSex = ownerSex;
            Guid = guid;
            Cont = cont;
            CreateTime = createTime;
        }

        public static List<ClientMessage> CreateClientMessages(JObject jsonJObject, string path, string pathInside,string mess="Cont")
        {
            List<ClientMessage> tempMessages = new List<ClientMessage>();
            foreach (var data in jsonJObject[path])
            {
                tempMessages.Add(new ClientMessage(
                    data["UserInfo"]["Guid"].ToString(),
                    data["UserInfo"]["Account"].ToString(),
                    data["UserInfo"]["Avatar"].ToString(),
                    data["UserInfo"]["Sex"].ToString(),
                    data[pathInside]["Guid"].ToString(),
                    data[pathInside][mess].ToString(),
                    DateTime.Parse(data[pathInside]["Time"].ToString())));
            }
            return tempMessages;
        }
    }
}
