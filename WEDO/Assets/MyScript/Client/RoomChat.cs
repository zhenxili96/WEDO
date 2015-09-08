using System;

namespace Wedo_ClientSide
{
    public class RoomChat
    {
        public string ChatGuid { get; private set; }
        public string Content { get; private set; }
        public string UserNickName { get; private set; }
        public DateTime Time { get; private set; }
        public int IsRecord { get; private set; }

        public RoomChat(string chatGuid, string content, string userNickName, DateTime time, int isRecord)
        {
            ChatGuid = chatGuid;
            Content = content;
            UserNickName = userNickName;
            Time = time;
            IsRecord = isRecord;
        }
    }
}
