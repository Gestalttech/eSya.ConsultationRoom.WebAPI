using eSya.ConsultationRoom.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConsultationRoom.IF
{
    public interface IConsultationRoomRepository
    {
        #region Consultation Room Header
        Task<List<DO_Lounge>> GetLoungebyBusinessKey(int businesskey);
        Task<List<DO_Floor>> GetFloorsbyLounge(int businesskey, int lounge);
        Task<List<DO_Area>> GetAreasbyFloor(int businesskey, int lounge, int floorId);
        Task<List<DO_ConsultationRoomHeader>> GetConsultationRoomHeadersbyBusinessKey(int businesskey);
        Task<DO_ReturnParameter> InsertOrUpdateIntoConsultationRoomsHeader(DO_ConsultationRoomHeader obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveConsultationRooms(bool status, int businesskey, int loungekey);
        #endregion

        #region Consultation Room Details
        Task<List<DO_ConsultationRoomDetails>> GetConsultationRoomDetailsbyLoungekey(int businesskey, int loungekey);
        Task<DO_ReturnParameter> InsertOrUpdateIntoConsultationRoomsDetail(DO_ConsultationRoomDetails obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveConsultationRoomsDetail(bool status, int businesskey, int loungekey, string consroomNo);
        #endregion

    }
}
