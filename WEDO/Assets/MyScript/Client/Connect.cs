using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Wedo_ClientSide
{
    static public class Connect
    {
        public static Socket ClientSocket;
        static public bool StartConnect()
        {
            for (int i = 0; i < StaticConfiguration.TryConnect; i++)
            {
                if (ClientSocket == null)
                {
                    IPAddress ip = IPAddress.Parse(StaticConfiguration.ServerIp);
                    ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        ClientSocket.Connect(new IPEndPoint(ip, StaticConfiguration.ServerPort)); //配置服务器IP与端口  
                        return true;
                    }
                    catch
                    {
                        ClientSocket = null;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static void EndConnect()
        {
            if (ClientSocket == null) return;
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            ClientSocket = null;
        }

        public static bool SendMessage(string sendMessage)
        {
            if (ClientSocket == null)
            {
                if (StartConnect() == false)
                    return false;
            }
            if (ClientSocket == null) return false;
            try
            {
                ClientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
            }
            catch
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                ClientSocket = null;
                return false;
            }
            return true;
        }

        public static string ReceiveMessage()
        {
            byte[] result = new byte[StaticConfiguration.MaxMessLength];
            //通过clientSocket接收数据  
            int receiveLength = ClientSocket.Receive(result);
            return Encoding.UTF8.GetString(result, 0, receiveLength);
        }
    }
}