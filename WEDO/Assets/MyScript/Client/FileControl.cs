using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    static public class FileControl
    {
        static public string SendFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                Connect.SendMessage(
                    JsonConvert.SerializeObject(
                        new
                        {
                            Oper = "UploadFile",
                            FileName = (Path.GetFileName(filePath)),
                            SizeCount = (fs.Length)
                        }));
                dynamic jsonObject = JObject.Parse(Connect.ReceiveMessage());
                if (jsonObject.Mess != "Start")
                    return null;
                BinaryReader br = new BinaryReader(fs);
                while (true)
                {
                    byte[] x = br.ReadBytes(StaticConfiguration.MaxMessLength);
                    Connect.ClientSocket.Send(x);
                    if (x.Length == 0)
                        break;
                }
                br.Close();
                fs.Close();
                jsonObject = JObject.Parse(Connect.ReceiveMessage());
                return jsonObject.Mess.ToString() == "Ok" ? jsonObject.NewName.ToString() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        static public bool ReceiveFile(string fileName, int sizeCount)
        {
            try
            {
                FileStream fs = new FileStream(StaticConfiguration.DwonloadFilePath + fileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                var result = new byte[StaticConfiguration.MaxMessLength];
                int size, sizeTotal = 0;
                while ((size = Connect.ClientSocket.Receive(result)) > 0)
                {
                    bw.Write(result, 0, size);
                    sizeTotal += size;
                    if (sizeTotal >= sizeCount)
                        break;
                }
                bw.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
