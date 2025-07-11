using System;
using System.Collections.Generic;

namespace eSya.ConsultationRoom.DL.Entities
{
    public partial class GtEccsrm
    {
        public int BusinessKey { get; set; }
        public int Lounge { get; set; }
        public int FloorId { get; set; }
        public int Area { get; set; }
        public int LoungeKey { get; set; }
        public int NoOfConsRoom { get; set; }
        public int RoomCount { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
