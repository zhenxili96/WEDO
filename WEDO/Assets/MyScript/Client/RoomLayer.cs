using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wedo_ClientSide
{
    class RoomLayer
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
