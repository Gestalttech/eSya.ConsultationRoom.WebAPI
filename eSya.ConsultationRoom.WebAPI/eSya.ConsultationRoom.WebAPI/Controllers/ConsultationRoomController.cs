using eSya.ConsultationRoom.DL.Repository;
using eSya.ConsultationRoom.DO;
using eSya.ConsultationRoom.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConsultationRoom.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsultationRoomController : ControllerBase
    {
        private readonly IConsultationRoomRepository _consultationRoomRepository;
        public ConsultationRoomController(IConsultationRoomRepository consultationRoomRepository)
        {
            _consultationRoomRepository = consultationRoomRepository;
        }
        #region Consultation Room Header
        
        [HttpGet]
        public async Task<IActionResult> GetLoungebyBusinessKey(int businesskey)
        {
            var ds = await _consultationRoomRepository.GetLoungebyBusinessKey(businesskey);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetFloorsbyLounge(int businesskey, int lounge)
        {
            var ds = await _consultationRoomRepository.GetFloorsbyLounge(businesskey, lounge);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetAreasbyFloor(int businesskey, int lounge, int floorId)
        {
            var ds = await _consultationRoomRepository.GetAreasbyFloor(businesskey, lounge, floorId);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetConsultationRoomHeadersbyBusinessKey(int businesskey)
        {
            var ds = await _consultationRoomRepository.GetConsultationRoomHeadersbyBusinessKey(businesskey);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateIntoConsultationRoomsHeader(DO_ConsultationRoomHeader obj)
        {
            var ds = await _consultationRoomRepository.InsertOrUpdateIntoConsultationRoomsHeader(obj);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveConsultationRooms(bool status, int businesskey, int loungekey)
        {
            var ds = await _consultationRoomRepository.ActiveOrDeActiveConsultationRooms(status,businesskey, loungekey);
            return Ok(ds);
        }
        #endregion

        #region Consultation Room Details

        [HttpGet]
        public async Task<IActionResult> GetConsultationRoomDetailsbyLoungekey(int businesskey, int loungekey)
        {
            var ds = await _consultationRoomRepository.GetConsultationRoomDetailsbyLoungekey(businesskey, loungekey);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateIntoConsultationRoomsDetail(DO_ConsultationRoomDetails obj)
        {
            var ds = await _consultationRoomRepository.InsertOrUpdateIntoConsultationRoomsDetail(obj);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveConsultationRoomsDetail(bool status, int businesskey, int loungekey, string consroomNo)
        {
            var ds = await _consultationRoomRepository.ActiveOrDeActiveConsultationRoomsDetail(status, businesskey, loungekey, consroomNo);
            return Ok(ds);
        }
        #endregion
    }
}
