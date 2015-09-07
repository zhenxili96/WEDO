using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wedo_ClientSide
{
    class RoomInterface
    {
        public Guid ProjectGuid { get; private set; }
        public Guid NowUserGuid { get; private set; }
        public RoomUser NowUser { get; private set; }
        public List<RoomUser> RoomUsers { get; private set; }
        public List<RoomLayer> RoomLayers { get; private set; }
        public List<RoomChat> RoomChats { get; private set; }
        private List<string> _operationsList;
        private Thread _updateThread;
        private bool _flagEnd;

        private RoomInterface(Guid projectGuid, Guid nowUserGuid, JObject infos)
        {
            ProjectGuid = projectGuid;
            NowUserGuid = nowUserGuid;
            RoomUsers = new List<RoomUser>();
            RoomLayers = new List<RoomLayer>();
            RoomChats = new List<RoomChat>();
            NowUser = null;
            _operationsList = new List<string>();
            UpdateInfo(infos);
            StartLoop();
        }

        private void LoopUpdate()
        {
            try
            {
                while (_flagEnd)
                {
                    Thread.Sleep(300);
                    if (_operationsList.Count == 0)
                    {
                        Connect.SendMessage(
                            BaseProcess(new Dictionary<string, object>
                            {
                                {"Oper", "UpdateRoom"},
                                {"ProjectGuid", ProjectGuid.ToString()}
                            }));
                        UpdateInfo(JObject.Parse(Connect.ReceiveMessage()));
                    }
                    else
                    {
                        Connect.SendMessage(_operationsList[0]);
                        _operationsList.RemoveAt(0);
                        UpdateInfo(JObject.Parse(Connect.ReceiveMessage()));
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void StartLoop()
        {
            _flagEnd = true;
            _updateThread = new Thread(LoopUpdate);
            _updateThread.Start();
        }

        private void EndLoop()
        {
            _flagEnd = false;
        }

        private bool UpdateInfo(JObject infos)
        {
            try
            {
                if (infos["Mess"].ToString() != "Ok")
                {
                    NowUser = null;
                    RoomUsers = null;
                    RoomChats = null;
                    RoomLayers = null;
                }
                RoomUser tempNowUser = null;
                var tempRoomUsers = new List<RoomUser>();
                var tempRoomLayers = new List<RoomLayer>();
                var tempRoomChats = new List<RoomChat>();

                foreach (var x in infos["Infos"]["RoomUsers"])
                {
                    RoomUser temp = new RoomUser(
                        x["NowUser"]["Guid"].ToString(),
                        x["NowUser"]["Account"].ToString(),
                        float.Parse(x["LeftCoordX"].ToString()),
                        float.Parse(x["LeftCoordY"].ToString()),
                        float.Parse(x["LeftCoordZ"].ToString()),
                        float.Parse(x["RightCoordX"].ToString()),
                        float.Parse(x["RightCoordY"].ToString()),
                        float.Parse(x["RightCoordZ"].ToString()),
                        int.Parse(x["Color"].ToString()),
                        x["NowUser"]["Avatar"].ToString());
                    if (x["NowUser"]["Guid"].ToString() == NowUserGuid.ToString())
                        tempNowUser = temp;
                    else
                        tempRoomUsers.Add(temp);
                }
                foreach (var x in infos["Infos"]["RoomLayers"])
                {
                    ClientLayer tempClientLayer = new ClientLayer(
                        x["NowLayer"]["Guid"].ToString(),
                        int.Parse(x["NowLayer"]["Number"].ToString()),
                        float.Parse(x["NowLayer"]["CoordX"].ToString()),
                        float.Parse(x["NowLayer"]["CoordY"].ToString()),
                        float.Parse(x["NowLayer"]["CoordZ"].ToString()));
                    List<ClientMaterial> tempClientMaterials = new List<ClientMaterial>();
                    foreach (var y in x["BoardMaterials"])
                    {
                        ClientMaterial tempClientMaterial = new ClientMaterial(
                            y["Guid"].ToString(),
                            y["BelongLevel"].ToString(),
                            float.Parse(y["CoordX"].ToString()),
                            float.Parse(y["CoordY"].ToString()),
                            float.Parse(y["CoordZ"].ToString()),
                            float.Parse(y["ScalingX"].ToString()),
                            float.Parse(y["ScalingY"].ToString()),
                            float.Parse(y["ScalingZ"].ToString()),
                            float.Parse(y["RotateX"].ToString()),
                            float.Parse(y["RotateY"].ToString()),
                            float.Parse(y["RotateZ"].ToString()),
                            y["Color"].ToString(),
                            int.Parse(y["Type"].ToString()),
                            y["Cont"].ToString(),
                            int.Parse(y["FontSize"].ToString()),
                            y["Font"].ToString()
                            );
                        tempClientMaterials.Add(tempClientMaterial);
                    }
                    RoomLayer temp = new RoomLayer(tempClientLayer, tempClientMaterials);
                    tempRoomLayers.Add(temp);
                }
                foreach (var x in infos["Infos"]["RoomChats"])
                {
                    RoomChat temp = new RoomChat(
                        x["ChatGuid"].ToString(),
                        x["Content"].ToString(),
                        x["UserNickName"].ToString(),
                        Convert.ToDateTime(x["Time"].ToString()),
                        int.Parse(x["IsRecord"].ToString()));
                    tempRoomChats.Add(temp);
                }
                NowUser = tempNowUser;
                RoomUsers = tempRoomUsers;
                RoomChats = tempRoomChats;
                RoomLayers = tempRoomLayers;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改自身手势
        /// </summary>
        /// <param name="leftCoordX">左手坐标</param>
        /// <param name="leftCoordY"></param>
        /// <param name="leftCoordZ"></param>
        /// <param name="rightCoordX">右手坐标</param>
        /// <param name="rightCoordY"></param>
        /// <param name="rightCoordZ"></param>
        /// <param name="color">颜色</param>
        public void EditUser(float leftCoordX, float leftCoordY, float leftCoordZ, float rightCoordX, float rightCoordY, float rightCoordZ, int color)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "EditUser"},
                {"Guid",NowUserGuid.ToString()},
                {"LeftCoordX",leftCoordX},
                {"LeftCoordY",leftCoordY},
                {"LeftCoordZ",leftCoordZ},
                {"RightCoordX",rightCoordX},
                {"RightCoordX",rightCoordY},
                {"RightCoordX",rightCoordZ},
                {"Color",color},
            }));
        }

        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="cont">内容</param>
        public void AddChat(string cont)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "AddChat"},
                {"Guid",NowUserGuid.ToString()},
                {"Cont",cont}
            }));
        }

        /// <summary>
        /// 将聊天信息升级成记录
        /// </summary>
        /// <param name="chatGuid"></param>
        public void ChatToRecord(string chatGuid)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "ChatToRecord"},
                {"ChatGuid",chatGuid}
            }));
        }

        /// <summary>
        /// 修改素材
        /// </summary>
        /// <param name="boardMaterialGuid"></param>
        /// <param name="layerGuid"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        /// <param name="coordZ"></param>
        /// <param name="scalingX"></param>
        /// <param name="scalingY"></param>
        /// <param name="scalingZ"></param>
        /// <param name="rotateX"></param>
        /// <param name="rotateY"></param>
        /// <param name="rotateZ"></param>
        /// <param name="color"></param>
        /// <param name="type"></param>
        /// <param name="cont"></param>
        /// <param name="fontSize"></param>
        /// <param name="font"></param>
        public void EditBoardMaterial(string boardMaterialGuid, string layerGuid, float coordX, float coordY, float coordZ, float scalingX, float scalingY, float scalingZ, float rotateX, float rotateY, float rotateZ, string color, int type, string cont, int fontSize, string font)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "EditBoardMaterial"},
                {"BoardMaterialGuid",boardMaterialGuid},
                {"LayerGuid",layerGuid},
                {"CoordX",coordX},
                {"CoordY",coordY},
                {"CoordZ",coordZ},
                {"ScalingX",scalingX},
                {"ScalingY",scalingY},
                {"ScalingZ",scalingZ},
                {"RotateX",rotateX},
                {"RotateY",rotateY},
                {"RotateZ",rotateZ},
                {"Color",color},
                {"Type",type},
                {"Cont",cont},
                {"FontSize",fontSize},
                {"Font",font}
            }));
        }

        /// <summary>
        /// 编辑图层
        /// </summary>
        /// <param name="layerGuid"></param>
        /// <param name="number"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        /// <param name="coordZ"></param>
        public void EditLayer(string layerGuid, int number, float coordX, float coordY, float coordZ)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "EditLayer"},
                {"LayerGuid",layerGuid},
                {"Number",number},
               {"CoordX",coordX},
                {"CoordY",coordY},
                {"CoordZ",coordZ}
            }));
        }

        /// <summary>
        /// 添加图层
        /// </summary>
        /// <param name="number"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        /// <param name="coordZ"></param>
        public void AddLayer(int number, float coordX, float coordY, float coordZ)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "AddLayer"},
                {"Number", number},
                {"CoordX", coordX},
                {"CoordY", coordY},
                {"CoordZ", coordZ}
            }));
        }

        /// <summary>
        /// 删除图层
        /// </summary>
        /// <param name="layerGuid"></param>
        public void DeleteLayer(string layerGuid)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "DeleteLayer"},
                {"LayerGuid",layerGuid}
            }));
        }

        /// <summary>
        /// 添加素材
        /// </summary>
        /// <param name="layerGuid"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        /// <param name="coordZ"></param>
        /// <param name="scalingX"></param>
        /// <param name="scalingY"></param>
        /// <param name="scalingZ"></param>
        /// <param name="rotateX"></param>
        /// <param name="rotateY"></param>
        /// <param name="rotateZ"></param>
        /// <param name="color"></param>
        /// <param name="type"></param>
        /// <param name="cont"></param>
        /// <param name="fontSize"></param>
        /// <param name="font"></param>
        public void AddBoardMaterial(string layerGuid, float coordX, float coordY, float coordZ, float scalingX, float scalingY, float scalingZ, float rotateX, float rotateY, float rotateZ, string color, int type, string cont, int fontSize, string font)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "AddBoardMaterial"},
                {"LayerGuid",layerGuid},
                {"CoordX",coordX},
                {"CoordY",coordY},
                {"CoordZ",coordZ},
                {"ScalingX",scalingX},
                {"ScalingY",scalingY},
                {"ScalingZ",scalingZ},
                {"RotateX",rotateX},
                {"RotateY",rotateY},
                {"RotateZ",rotateZ},
                {"Color",color},
                {"Type",type},
                {"Cont",cont},
                {"FontSize",fontSize},
                {"Font",font}
            }));
        }

        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="layerGuid">素材所属图层Guid</param>
        /// <param name="boardMaterialGuid"></param>
        public void DeleteBoardMaterial(string layerGuid, string boardMaterialGuid)
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "DeleteBoardMaterial"},
                {"LayerGuid",layerGuid},
                {"BoardMaterialGuid",boardMaterialGuid}
            }));
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _operationsList.Add(BaseProcess(new Dictionary<string, object>
            {
                {"Oper", "UpdateRoom"},
                {"ProjectGuid", ProjectGuid.ToString()},
                {"RoomOper", "Save"}
            }));
        }

        /// <summary>
        /// 退出房间
        /// </summary>
        /// <returns>成功与否</returns>
        public bool ExitRoom()
        {
            try
            {
                EndLoop();
                Connect.SendMessage(
                    BaseProcess(new Dictionary<string, object>
                {
                    {"Oper", "ExitRoom"},
                    {"Guid", Guid.Empty.ToString()},
                    {"ProjectGuid", ProjectGuid}
                }));
                JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                return jsonJObject["Mess"].ToString() == "Ok";
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭房间
        /// </summary>
        /// <returns>成功与否</returns>
        public bool CloseRoom()
        {
            try
            {
                EndLoop();
                Connect.SendMessage(
                    BaseProcess(new Dictionary<string, object>
                {
                    {"Oper", "CloseRoom"},
                    {"Guid", Guid.Empty.ToString()},
                    {"ProjectGuid", ProjectGuid.ToString()}
                }));
                JObject jsonJObject = JObject.Parse(Connect.ReceiveMessage());
                return jsonJObject["Mess"].ToString() == "Ok";
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <returns>返回null则失败</returns>
        static public RoomInterface CreateRoom(string guid, string projectGuid)
        {
            Connect.SendMessage(
                BaseProcess(new Dictionary<string, object>
                {
                    {"Oper", "CreateRoom"},
                    {"Guid", guid},
                    {"ProjectGuid", projectGuid}
                }));
            JObject x = JObject.Parse(Connect.ReceiveMessage());
            return x["Mess"].ToString() != "Ok" ? null : new RoomInterface(new Guid(projectGuid), new Guid(guid), x);
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="guid">用户Guid</param>
        /// <param name="projectGuid">项目Guid</param>
        /// <returns>返回null则失败</returns>
        static public RoomInterface EnterRoom(string guid, string projectGuid)
        {
            Connect.SendMessage(
                BaseProcess(new Dictionary<string, object>
                {
                    {"Oper", "EnterRoom"},
                    {"Guid", guid},
                    {"ProjectGuid", projectGuid}
                }));
            JObject x = JObject.Parse(Connect.ReceiveMessage());
            return x["Mess"].ToString() != "Ok" ? null : new RoomInterface(new Guid(projectGuid), new Guid(guid), x);
        }

        static private string BaseProcess(Dictionary<string, object> jsonMess)
        { return JsonConvert.SerializeObject(jsonMess); }
    }
}
