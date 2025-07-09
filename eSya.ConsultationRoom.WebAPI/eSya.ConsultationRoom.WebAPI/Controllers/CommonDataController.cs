using eSya.ConsultationRoom.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConsultationRoom.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonDataController : ControllerBase
    {
        private readonly ICommonDataRepository _commondataRepository;
        public CommonDataController(ICommonDataRepository commondataRepository)
        {
            _commondataRepository = commondataRepository;
        }
        /// <summary>
        /// Getting  Business Key.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var ds = await _commondataRepository.GetBusinessKey();
            return Ok(ds);
        }
        /// <summary>
        /// Approval Level & Type.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            var ds = await _commondataRepository.GetApplicationCodesByCodeTypeList(l_codeType);
            return Ok(ds);
        }
    }
}
