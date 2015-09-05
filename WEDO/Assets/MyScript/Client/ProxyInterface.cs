using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    static public class ProxyInterface
    {
        static private readonly object LockObj = new object();

        #region Operations
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="avatar">头像</param>
        /// <param name="mailBox">邮箱</param>
        /// <param name="sex">性别，0为男，1为女</param>
        /// <returns>用户信息</returns>
        static public ClientUser User_Register(string account, string password, string avatar, string mailBox, string sex)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "Register"},
                        {"Account", account},
                        {"Password", password},
                        {"Avatar", avatar},
                        {"MailBox", mailBox},
                        {"Sex", sex}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    if (jsonJObject["Mess"].ToString() == "Ok")
                    {
                        return new ClientUser(
                            jsonJObject["User"]["Guid"].ToString(),
                            jsonJObject["User"]["Account"].ToString(),
                            jsonJObject["User"]["Avatar"].ToString(),
                            jsonJObject["User"]["MailBox"].ToString(),
                            jsonJObject["User"]["Sex"].ToString(),
                            new List<ClientProject>(), 
                            new List<ClientNotification>());
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>
        static public ClientUser User_Login(string account, string password)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "Login"},
                        {"Account", account},
                        {"Password", password}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok" ? ClientUser.CreateClientUser(jsonJObject) : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户详细信息（项目，邮箱）
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <returns>详细信息</returns>
        static public ClientUser User_GetDetail(string guid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetUserDetail"},
                        {"Guid", guid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok" ? ClientUser.CreateClientUser(jsonJObject) : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 修改用户基础信息
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <param name="avatar">修改后头像</param>
        /// <param name="mailBox">邮箱</param>
        /// <param name="sex">性别，0为男，1为女</param>
        /// <returns>成功与否</returns>
        static public bool User_EditBaseInfo(string guid, string avatar, string mailBox, string sex)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "EditBaseInfo"},
                        {"Guid", guid},
                        {"Avatar", avatar},
                        {"MailBox", mailBox},
                        {"Sex", sex}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>成功与否</returns>
        static public bool User_EditPassword(string guid, string oldPassword, string newPassword)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "EditPassword"},
                        {"Guid", guid},
                        {"OldPassword", oldPassword},
                        {"NewPassword", newPassword}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <param name="name">项目名</param>
        /// <param name="parentGuid">项目父节点Guid，null为空</param>
        /// <returns>项目信息（信息内的OwnerGuid，OwnerAccount，OwnerAvatar为空）</returns>
        static public ClientProject Project_Create(string guid, string name, string parentGuid = null)
        {
            try
            {
                lock (LockObj)
                {
                    if (parentGuid == null)
                    {
                        Connect.SendMessage(
                            BaseProcess(new Dictionary<string, object>
                        {
                            {"Oper", "CreateProject"},
                            {"Guid", guid},
                            {"Name", name}
                        }));
                    }
                    else
                    {
                        Connect.SendMessage(
                            BaseProcess(new Dictionary<string, object>
                        {
                            {"Oper", "CreateProject"},
                            {"Guid", guid},
                            {"Name", name},
                            {"ParentGuid", parentGuid}
                        }));
                    }
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? new ClientProject(
                            "", "", "",
                            jsonJObject["Project"]["Guid"].ToString(),
                            jsonJObject["Project"]["Name"].ToString(),
                            jsonJObject["Project"]["ParentID"].ToString(),
                            DateTime.Parse(jsonJObject["Project"]["CreateTime"].ToString()))
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有项目（顶级项目）
        /// </summary>
        /// <returns>项目信息及发起人信息数组</returns>
        static public List<ClientProject> Project_GetAll()
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetAllProjects"}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientProject.CreateClientProjects(jsonJObject, "Projects")
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户的项目（顶级项目）
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <returns>项目信息</returns>
        static public List<ClientProject> Project_ByUser(string guid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetUserProjects"},
                        {"Guid", guid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientProject.CreateClientProjects(jsonJObject, "Projects")
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取项目的子项目
        /// </summary>
        /// <param name="projectGuid">项目Guid</param>
        /// <returns>项目信息</returns>
        static public List<ClientProject> Project_GetChildren(string projectGuid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetProjectChildren"},
                        {"ProjectGuid", projectGuid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientProject.CreateClientProjects(jsonJObject, "Projects")
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取项目详细信息
        /// </summary>
        /// <param name="projectGuid">项目Guid</param>
        /// <returns>详细信息</returns>
        static public ClientProjectDetail Project_GetInfo(string projectGuid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetProjectInfo"},
                        {"ProjectGuid", projectGuid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientProjectDetail.CreateClientProjectDetail(jsonJObject)
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 申请项目
        /// </summary>
        /// <param name="guid">申请人Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="reason">申请原因</param>
        /// <returns>操作成功与否</returns>
        static public bool Application_Apply(string guid, string projectGuid, string reason)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "ApplyProject"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"Reason", reason}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取项目申请人列表
        /// </summary>
        /// <param name="projectGuid">项目Guid</param>
        /// <returns>申请信息与申请人列表</returns>
        static public List<ClientMessage> Application_GetAll(string projectGuid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "GetAllApplications"},
                        {"ProjectGuid", projectGuid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientMessage.CreateClientMessages(jsonJObject, "Applications", "Application", "Reason")
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 处理申请
        /// </summary>
        /// <param name="guid">处理人Guid</param>
        /// <param name="applicationGuid">申请Guid，即为ClientMessage.Guid</param>
        /// <param name="isPass">是否通过，true为通过</param>
        /// <returns>成功与否</returns>
        static public bool Application_Handle(string guid, string applicationGuid, bool isPass)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "HandleApplication"},
                        {"Guid", guid},
                        {"ApplicationGuid", applicationGuid},
                        {"IsPass", (isPass ? "1" : "0")}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置或取消管理员
        /// </summary>
        /// <param name="guid">设置人（必须是项目拥有者）</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="setUserGuid">设置对象Guid</param>
        /// <param name="isSet">设置或取消，true为设置，false为取消</param>
        /// <returns>成功与否</returns>
        static public bool Admin_SetOrUnset(string guid, string projectGuid, string setUserGuid, bool isSet)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "SetOrUnsetAdmin"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"SetUserGuid", setUserGuid},
                        {"IsSet", (isSet ? "1" : "0")}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 踢人（只有项目拥有者能踢人）
        /// </summary>
        /// <param name="guid">操作人Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="kickUserGuid">要踢对象Guid</param>
        /// <returns>成功与否</returns>
        static public bool Admin_KickUser(string guid, string projectGuid, string kickUserGuid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "KickUser"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"KickUserGuid", kickUserGuid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="notificationGuid">消息Guid</param>
        /// <param name="isAccept">是否同意，消息类型为2时才需要</param>
        /// <returns>成功与否</returns>
        static public bool Notification_SetIsRead(string notificationGuid, bool? isAccept = null)
        {
            try
            {
                lock (LockObj)
                {
                    if (isAccept == null)
                    {
                        Connect.SendMessage(
                            BaseProcess(new Dictionary<string, object>
                        {
                            {"Oper", "SetIsRead"},
                            {"NotificationGuid", notificationGuid}
                        }));
                    }
                    else
                    {
                        Connect.SendMessage(
                            BaseProcess(new Dictionary<string, object>
                        {
                            {"Oper", "SetIsRead"},
                            {"NotificationGuid", notificationGuid},
                            {"IsAccept", isAccept==true?"1":"0"}
                        }));
                    }
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加公告（管理员或者项目发起者）
        /// </summary>
        /// <param name="guid">操作人Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="cont">公告内容</param>
        /// <returns>成功与否</returns>
        static public bool Announcement_Add(string guid, string projectGuid, string cont)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "AddAnnouncement"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"Cont", cont}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="guid">说话用户Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="cont">内容</param>
        /// <returns>成功时返回所有聊天信息</returns>
        static public List<ClientMessage> Chat_Add(string guid, string projectGuid, string cont)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "AddChat"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"Cont", cont}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok"
                        ? ClientMessage.CreateClientMessages(jsonJObject, "ChatInfos", "ChatInfo")
                        : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 发送邀请
        /// </summary>
        /// <param name="guid">发送人Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <param name="toGuid">发送对象Guid</param>
        /// <returns>成功与否</returns>
        static public bool Invitation_Send(string guid, string projectGuid, string toGuid)
        {
            try
            {
                lock (LockObj)
                {
                    Connect.SendMessage(
                        BaseProcess(new Dictionary<string, object>
                    {
                        {"Oper", "SendInvitation"},
                        {"Guid", guid},
                        {"ProjectGuid", projectGuid},
                        {"ToGuid", toGuid}
                    }));
                    JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                    return jsonJObject["Mess"].ToString() == "Ok";
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  发送文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>发送成功后的文件名</returns>
        static public string File_Upload(string filePath)
        {
            try
            {
                lock (LockObj)
                {
                    return FileControl.SendFile(filePath);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 请求文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否请求成功，成功后文件将保存在“DwonloadFiles/...”</returns>
        static public bool File_Request(string fileName)
        {
            try
            {
                if (File.Exists(StaticConfiguration.DwonloadFilePath + fileName))
                    return true;
                lock (LockObj)
                {
                    Connect.SendMessage(BaseProcess(new Dictionary<string, object> { { "Oper", "RequestFile" }, { "FileName", fileName } }));
                    JObject jsonObject = JObject.Parse(Connect.ReceiveMessage());
                    if (jsonObject["Mess"].ToString() != "Ok")
                        return false;
                    Connect.SendMessage(BaseProcess(new Dictionary<string, object> { { "Mess", "Start" } }));
                    return FileControl.ReceiveFile(jsonObject["FileName"].ToString(), int.Parse(jsonObject["SizeCount"].ToString()));
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Others
        static private string BaseProcess(Dictionary<string, object> jsonMess)
        { return JsonConvert.SerializeObject(jsonMess); }

        /// <summary>
        /// 开始请求时调用，可不调用（自动调用）
        /// </summary>
        /// <returns></returns>
        static public bool Connect_Start()
        { return Connect.StartConnect(); }

        /// <summary>
        /// 关闭客户端时调用
        /// </summary>
        static public void Connect_End()
        { Connect.EndConnect(); }
        #endregion
    }
}
