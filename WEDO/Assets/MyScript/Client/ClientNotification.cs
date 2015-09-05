using System;
using System.Collections.Generic;
using System.Text;

namespace Wedo_ClientSide
{
    public class ClientNotification
    {
        public string Guid { get; private set; }
        public string Message { get; private set; }
        /// <summary>
        /// 消息类型，1为普通系统消息，2为邀请消息（发送Notification_SetIsRead时需要IsAccept参数）
        /// </summary>
        public string NotificationType { get; private set; }
        /// <summary>
        /// 是否阅读，1为已阅，0为未阅
        /// </summary>
        public string IsRead { get; private set; }
        public DateTime SetTime { get; private set; }

        public ClientNotification(string guid, string message, string notificationType, string isRead, DateTime setTime)
        {
            Guid = guid;
            Message = message;
            NotificationType = notificationType;
            IsRead = isRead;
            SetTime = setTime;
        }
    }
}
