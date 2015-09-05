using System;

namespace Wedo_ClientSide
{
    static class StaticConfiguration
    {
        //服务器ip
        public const String ServerIp = "120.24.81.199";
        //服务器端口号
        public const int ServerPort = 8885;
        //尝试连接服务器上限次数
        public const int TryConnect = 5;
        //一次传输信息最大byte
        public const int MaxMessLength = 32768;
        //上传文件存储路径
        public const string DwonloadFilePath = "DwonloadFiles/";
    }
}
