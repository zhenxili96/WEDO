using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    public class ClientMenber
    {
        public ClientUser NowUser { get; private set; }
        /// <summary>
        /// 成员类型，1为普通成员，2为管理员
        /// </summary>
        public string MemberType { get; private set; }
        /// <summary>
        /// 参加时间
        /// </summary>
        public DateTime JoinTime { get; private set; }

        public ClientMenber(ClientUser nowUser, string memberType, DateTime joinTime)
        {
            NowUser = nowUser;
            MemberType = memberType;
            JoinTime = joinTime;
        }

        public static List<ClientMenber> CreateClientMenbers(JObject jsonJObject)
        {
            List<ClientMenber> temp = new List<ClientMenber>();
            foreach (var data in jsonJObject["Menbers"])
            {
                temp.Add(new ClientMenber(
                    new ClientUser(
                        data["UserInfo"]["Guid"].ToString(),
                        data["UserInfo"]["Account"].ToString(),
                        data["UserInfo"]["Avatar"].ToString(),
                        data["UserInfo"]["MailBox"].ToString(),
                        data["UserInfo"]["Sex"].ToString(),
                        null, null),
                    data["Participation"]["MemberType"].ToString(),
                    DateTime.Parse(data["Participation"]["Time"].ToString())
                    ));
            }
            return temp;
        } 
    }
}
