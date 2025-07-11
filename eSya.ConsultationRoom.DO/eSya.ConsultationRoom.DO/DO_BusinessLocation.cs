using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConsultationRoom.DO
{
    public class DO_BusinessLocation
    {
        public int BusinessKey { get; set; }
        public string LocationDescription { get; set; }

    }
    public class DO_ApplicationCodes
    {
        public int ApplicationCode { get; set; }
        public int CodeType { get; set; }
        public string CodeDesc { get; set; }
    }
    public class DO_Floor
    {
        public int FloorId { get; set; }
        public string FloorName { get; set; }
    }
    public class DO_Lounge
    {
        public int LoungeID { get; set; }
        public string LoungeName { get; set; }
    }
    public class DO_Area
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
    }
}
