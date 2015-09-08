using System.Collections.Generic;

namespace Wedo_ClientSide
{
    public class RoomLayer
    {
        public ClientLayer NowLayer { get; private set; }
        public List<ClientMaterial> BoardMaterials { get; private set; }

        public RoomLayer(ClientLayer nowLayer, List<ClientMaterial> boardMaterials)
        {
            NowLayer = nowLayer;
            BoardMaterials = boardMaterials;
        }
    }
}
