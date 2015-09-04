namespace Wedo_ClientSide
{
    class RoomUser
    {
        public string UserGuid { get; private set; }
        public string UserNickName { get; private set; }
        public string UserAvatar { get; private set; }
        public float LeftCoordX { get; private set; }
        public float LeftCoordY { get; private set; }
        public float LeftCoordZ { get; private set; }
        public float RightCoordX { get; private set; }
        public float RightCoordY { get; private set; }
        public float RightCoordZ { get; private set; }
        public int Color { get; private set; }

        public RoomUser(string userGuid, string userNickName, float leftCoordX, float leftCoordY, float leftCoordZ, float rightCoordX, float rightCoordY, float rightCoordZ, int color, string userAvatar)
        {
            UserGuid = userGuid;
            UserNickName = userNickName;
            LeftCoordX = leftCoordX;
            LeftCoordY = leftCoordY;
            LeftCoordZ = leftCoordZ;
            RightCoordX = rightCoordX;
            RightCoordY = rightCoordY;
            RightCoordZ = rightCoordZ;
            Color = color;
            UserAvatar = userAvatar;
        }
    }
}
