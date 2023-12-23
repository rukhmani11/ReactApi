using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("MeetingRisk")]
    public class MeetingRiskController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingRiskService _meetingRiskService;
        #endregion

        #region Constructor
        public MeetingRiskController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingRiskService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _meetingRiskService = businessSegmentsService;
        }
        #endregion


        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingRiskDTO model)
        {

            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _meetingRiskService.AddMeetingRisk(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _meetingRiskService.GetAllMeetingRisk();
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(MeetingRiskDTO model)
        {
            //if (_meetingRiskService.IsMeetingRiskExists(model.Name.Trim(), model.Id))
            //{
            //    return BadRequest(new { isSuccess = false, message = "MeetingRisk already exists." });
            //}
            model.UpdatedById = currentUser.Id;
            Guid? id = await _meetingRiskService.EditMeetingRisk(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [Route("GetMeetingRisksByMeetingId/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetMeetingRisksByMeetingId(Guid meetingId)
        {
            var list = await _meetingRiskService.GetMeetingRisksByMeetingId(meetingId);
            return Ok(new { isSuccess = true, list = list });
        }

        [HttpDelete("{meetingRiskId}")]
        public async Task<ActionResult> Delete(Guid meetingRiskId)
        {
            var isDeleted = await _meetingRiskService.DeleteMeetingRisk(meetingRiskId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{meetingRiskId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid meetingRiskId)
        {
            var data = await _meetingRiskService.GetMeetingRiskById(meetingRiskId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("test")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult test()
        {
            var data = _meetingRiskService.spTest();
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }


        [Route("GetPendingMeetingRisksByClientIdOrClientBusinessUnitId")]
        [HttpGet]
        public async Task<IActionResult> GetPendingMeetingRisksByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? businessUnitId)
        {
            var res = await _meetingRiskService.GetPendingMeetingRisksByClientIdOrClientBusinessUnitId(clientId, businessUnitId);
            return Ok(new { isSuccess = true, list = res });
        }

        #endregion
    }
}
