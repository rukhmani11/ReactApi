using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("MeetingOpportunity")]
    public class MeetingOpportunityController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingOpportunityService _meetingOpportunityService;
        #endregion

        #region Constructor
        public MeetingOpportunityController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingOpportunityService meetingOpportunityService) : base(contextAccessor)
        {
            _context = context;
            _meetingOpportunityService = meetingOpportunityService;
        }
        #endregion


        #region Methods

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingOpportunityDTO model)
        {

            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _meetingOpportunityService.AddMeetingOpportunity(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(MeetingOpportunityDTO model)
        {
          
            model.UpdatedById = currentUser.Id;
            Guid? id = await _meetingOpportunityService.EditMeetingOpportunity(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [HttpDelete("{meetingOpportunityId}")]
        public async Task<ActionResult> Delete(Guid meetingOpportunityId)
        {
            var isDeleted = await _meetingOpportunityService.DeleteMeetingOpportunity(meetingOpportunityId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetByMeetingId/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetByMeetingId(Guid meetingId)
        {
            var res = await _meetingOpportunityService.GetMeetingOpportunityByMeetingId(meetingId);
            return Ok(new { isSuccess = true, list = res });
        }

       

        [Route("GetById/{meetingOpportunityId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid meetingOpportunityId)
        {
            var data = await _meetingOpportunityService.GetMeetingOpportunityById(meetingOpportunityId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _meetingOpportunityService.GetAllMeetingOpportunity();
            return Ok(new { isSuccess = true, list = res });
        }

        //[Route("test")]
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult test()
        //{
        //    var data = _meetingOpportunityService.spTest();
        //    if (data == null)
        //    {
        //        return BadRequest(new { isSuccess = false, message = "No record found." });
        //    }
        //    return Ok(new { isSuccess = true, data = data });
        // }


        [Route("GetPendingMeetingOpportunityByClientIdOrClientBusinessUnitId")]
    [HttpGet]
    public async Task<IActionResult> GetPendingMeetingOpportunityByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? businessUnitId)
    {
        var res = await _meetingOpportunityService.GetPendingMeetingOpportunityByClientIdOrClientBusinessUnitId(clientId, businessUnitId);
        return Ok(new { isSuccess = true, list = res });
    }

        #endregion
    }
}

