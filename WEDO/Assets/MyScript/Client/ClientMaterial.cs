namespace Wedo_ClientSide
{
    public class ClientMaterial
    {
        public string Guid { get; private set; }
        public string BelongLevel { get; private set; }
        public float CoordX { get; private set; }
        public float CoordY { get; private set; }
        public float CoordZ { get; private set; }
        public float ScalingX { get; private set; }
        public float ScalingY { get; private set; }
        public float ScalingZ { get; private set; }
        public float RotateX { get; private set; }
        public float RotateY { get; private set; }
        public float RotateZ { get; private set; }
        public string Color { get; private set; }
        public int Type { get; private set; }
        public string Cont { get; private set; }
        public int FontSize { get; private set; }
        public string Font { get; private set; }

        public ClientMaterial(string guid, string belongLevel, float coordX, float coordY, float coordZ, float scalingX, float scalingY, float scalingZ, float rotateX, float rotateY, float rotateZ, string color, int type, string cont, int fontSize, string font)
        {
            Guid = guid;
            BelongLevel = belongLevel;
            CoordX = coordX;
            CoordY = coordY;
            CoordZ = coordZ;
            ScalingX = scalingX;
            ScalingY = scalingY;
            ScalingZ = scalingZ;
            RotateX = rotateX;
            RotateY = rotateY;
            RotateZ = rotateZ;
            Color = color;
            Type = type;
            Cont = cont;
            FontSize = fontSize;
            Font = font;
        }
    }
}
