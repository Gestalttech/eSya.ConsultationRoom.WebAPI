using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConsultationRoom.DO
{
    public class DO_ConsultationRoomHeader
    {
        public int BusinessKey { get; set; }
        public int Lounge { get; set; }
        public int FloorId { get; set; }
        public int Area { get; set; }
        public int LoungeKey { get; set; }
        public int NoOfConsRoom { get; set; }
        public int RoomCount { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string? LoungeDesc { get; set; }
        public string? FloorDesc { get; set; }
        public string? AreaDesc { get; set; }

    }
    public class DO_ConsultationRoomDetails
    {
        public int BusinessKey { get; set; }
        public int LoungeKey { get; set; }
        public string ConsultRoomNo { get; set; } 
        public string Remarks { get; set; } 
        public string RoomStatus { get; set; } 
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}
