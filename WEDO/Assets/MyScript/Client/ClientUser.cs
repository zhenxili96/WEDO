using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    public class ClientUser
    {
        public string Guid { get; private set; }
        public string Account { get; private set; }
        public string Avatar { get; private set; }
        public string MailBox { get; private set; }
        /// <summary>
        /// 性别，男为0，女为1
        /// </summary>
        public string Sex { get; private set; }
        public List<ClientProject> Projects { get; private set; }
        public List<ClientNotification> Notifications { get; private set; }

        public ClientUser(string guid, string account, string avatar, string mailBox, string sex, List<ClientProject> projects, List<ClientNotification> notifications)
        {
            Guid = guid;
            Account = account;
            Avatar = avatar;
            MailBox = mailBox;
            Sex = sex;
            Projects = projects;
            Notifications = notifications;
        }

        public static ClientUser CreateClientUser(JObject jsonJObject)
        {
            List<ClientProject> tempProjects = new List<ClientProject>();
            foreach (var project in jsonJObject["Projects"])
            {
                tempProjects.Add(new ClientProject(
                    project["UserInfo"]["Guid"].ToString(),
                    project["UserInfo"]["Account"].ToString(),
                    project["UserInfo"]["Avatar"].ToString(),
                    project["ProjectInfo"]["Guid"].ToString(),
                    project["ProjectInfo"]["Name"].ToString(),
                    project["ProjectInfo"]["ParentID"].ToString(),
                    DateTime.Parse(project["ProjectInfo"]["CreateTime"].ToString())));
            }
            List<ClientNotification> tempNotifications = new List<ClientNotification>();
            foreach (var data in jsonJObject["Notifications"])
            {
                tempNotifications.Add(new ClientNotification(
                    data["Guid"].ToString(),
                    data["Message"].ToString(),
                    data["NotificationType"].ToString(),
                    data["IsRead"].ToString(),
                    DateTime.Parse(data["SetTime"].ToString())));
            }
            return new ClientUser(
                jsonJObject["User"]["Guid"].ToString(),
                jsonJObject["User"]["Account"].ToString(),
                jsonJObject["User"]["Avatar"].ToString(),
                jsonJObject["User"]["MailBox"].ToString(),
                jsonJObject["User"]["Sex"].ToString(),
                tempProjects, tempNotifications);
        }
    }
}
