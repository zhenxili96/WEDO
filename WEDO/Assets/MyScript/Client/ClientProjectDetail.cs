using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    public class ClientProjectDetail
    {
        public ClientUser Owner { get; private set; }
        public List<ClientMenber> Menbers { get; private set; }
        public List<ClientProject> Children { get; private set; }
        public List<ClientMessage> Announcements { get; private set; }
        public List<ClientMessage> Records { get; private set; }
        public List<ClientMessage> ChatInfos { get; private set; }
        public bool IsInRoom { get; private set; }

        public ClientProjectDetail(ClientUser owner, List<ClientMenber> menbers, List<ClientProject> children, List<ClientMessage> announcements, List<ClientMessage> records, List<ClientMessage> chatInfos, bool isInRoom)
        {
            Owner = owner;
            Menbers = menbers;
            Announcements = announcements;
            Records = records;
            IsInRoom = isInRoom;
            ChatInfos = chatInfos;
            Children = children;
        }

        public static ClientProjectDetail CreateClientProjectDetail(JObject jsonJObject)
        {
            return new ClientProjectDetail(
                new ClientUser(
                jsonJObject["OwnerInfo"]["Guid"].ToString(),
                jsonJObject["OwnerInfo"]["Account"].ToString(),
                jsonJObject["OwnerInfo"]["Avatar"].ToString(),
                jsonJObject["OwnerInfo"]["MailBox"].ToString(),
                jsonJObject["OwnerInfo"]["Sex"].ToString(),
                null, null),
                ClientMenber.CreateClientMenbers(jsonJObject),
                ClientProject.CreateClientProjects(jsonJObject, "Children"),
                ClientMessage.CreateClientMessages(jsonJObject, "Announcements", "Announcement"),
                ClientMessage.CreateClientMessages(jsonJObject, "Records", "Record"),
                ClientMessage.CreateClientMessages(jsonJObject, "ChatInfos", "ChatInfo"),
                jsonJObject["IsInRoom"].ToString() == "1"
                );
        }
    }
}
