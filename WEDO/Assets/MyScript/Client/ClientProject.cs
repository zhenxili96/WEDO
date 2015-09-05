using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    public class ClientProject
    {
        public string OwnerGuid { get; private set; }
        public string OwnerAccount { get; private set; }
        public string OwnerAvatar { get; private set; }
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string ParentId { get; private set; }
        public DateTime CreateTime { get; private set; }

        public ClientProject(string ownerGuid, string ownerAccount, string ownerAvatar, string guid, string name, string parentId, DateTime createTime)
        {
            OwnerGuid = ownerGuid;
            OwnerAccount = ownerAccount;
            OwnerAvatar = ownerAvatar;
            Guid = guid;
            Name = name;
            ParentId = parentId;
            CreateTime = createTime;
        }

        public static List<ClientProject> CreateClientProjects(JObject jsonJObject, string path)
        {
            List<ClientProject> tempProjects = new List<ClientProject>();
            foreach (var project in jsonJObject[path])
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
            return tempProjects;
        }
    }
}
