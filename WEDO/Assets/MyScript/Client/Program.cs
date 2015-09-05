using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(ProxyInterface.Notification_SetIsRead("21c89f0d-9dcf-48fb-a873-63b3c87d0532",true));
            var x = ProxyInterface.Chat_Add("32b9697f-3d13-4f9a-b7af-5eeea93b16e4", "8764279b-b944-4afa-b57f-8e7c5977ed6d","大家好！-3");
            Console.WriteLine(x);
            //Console.WriteLine(x.User.Account.ToString());
            //Console.WriteLine(ProxyInterface.Invitation_Send("ce6e4a22-7865-491c-835e-c5b3e788c33a", "8764279b-b944-4afa-b57f-8e7c5977ed6d", "4b64c23d-3ada-4889-8284-39ee8c73a73d"));
            //RoomInterface x=RoomInterface.CreateRoom("ce6e4a22-7865-491c-835e-c5b3e788c33a", "8764279b-b944-4afa-b57f-8e7c5977ed6d");
            //Console.Read();
            //Console.WriteLine(x.CloseRoom());
            Console.Read();
        }
    }
}
