using System;

namespace Wedo_ClientSide
{
    class ClientLayer
    {
        public string Guid { get; private set; }
        public int Number { get; private set; }
        public float CoordX { get; private set; }
        public float CoordY { get; private set; }
        public float CoordZ { get; private set; }

        public ClientLayer(string guid, int number, float coordX, float coordY, float coordZ)
        {
            Guid = guid;
            Number = number;
            CoordX = coordX;
            CoordY = coordY;
            CoordZ = coordZ;
        }
    }
}
