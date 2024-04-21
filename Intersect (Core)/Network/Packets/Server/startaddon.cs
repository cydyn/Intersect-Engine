using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class startaddon : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public startaddon()
        {
        }
    }
}