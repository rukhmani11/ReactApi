using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("MeetingObservationAndOtherMatter")]
    public class MeetingObservationAndOtherMatterController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingObservationAndOtherMatterService _meetingObservationAndOtherMatterService;
        #endregion

        #region Constructor
        public MeetingObservationAndOtherMatterController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingObservationAndOtherMatterService meetingObservationAndOtherMatterService) : base(contextAccessor)
        {
            _context = context;
            _meetingObservationAndOtherMatterService = meetingObservationAndOtherMatterService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingObservationAndOtherMatterDTO model)
        {

            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _meetingObservationAndOtherMatterService.AddMeetingObservationAndOtherMatter(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(MeetingObservationAndOtherMatterDTO model)
        {
            
            model.UpdatedById = currentUser.Id;
            Guid? id = await _meetingObservationAndOtherMatterService.EditMeetingObservationAndOtherMatter(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }


        [HttpDelete("{meetingObservationAndOtherMatterId}")]
        public async Task<ActionResult> Delete(Guid meetingObservationAndOtherMatterId)
        {
            var isDeleted = await _meetingObservationAndOtherMatterService.DeleteMeetingObservationAndOtherMatter(meetingObservationAndOtherMatterId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetBymeetingObservationAndOtherMatterId/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetByMeetingId(Guid meetingId)
        {
            var res = await _meetingObservationAndOtherMatterService.GetMeetingObservationAndOtherMatterByMeetingId(meetingId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetById/{meetingObservationAndOtherMatterId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid MeetingObservationAndOtherMatterId)
        {
            var data = await _meetingObservationAndOtherMatterService.GetMeetingObservationAndOtherMatterById(MeetingObservationAndOtherMatterId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }


        [Route("GetPendingMeetingObservationAndOtherMatterByClientIdOrClientBusinessUnitId")]
        [HttpGet]
        public async Task<IActionResult> GetPendingMeetingObservationAndOtherMatterByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? businessUnitId)
        {
            var res = await _meetingObservationAndOtherMatterService.GetPendingMeetingObservationAndOtherMatterByClientIdOrClientBusinessUnitId(clientId, businessUnitId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _meetingObservationAndOtherMatterService.GetAllMeetingObservationAndOtherMatter();
            return Ok(new { isSuccess = true, list = res });
        }


        #endregion
    }
}
