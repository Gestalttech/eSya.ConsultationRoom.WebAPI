using System;
using System.Collections.Generic;

namespace eSya.ConsultationRoom.DL.Entities
{
    public partial class GtEacorm
    {
        public int BusinessKey { get; set; }
        public int LoungeKey { get; set; }
        public string ConsultRoomNo { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string RoomStatus { get; set; } = null!;
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
