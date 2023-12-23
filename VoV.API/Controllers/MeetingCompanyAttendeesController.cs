using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("MeetingCompanyAttendees")]
    public class MeetingCompanyAttendeesController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingCompanyAttendeesService _meetingCompanyAttendeesService;
        #endregion

        #region Constructor
        public MeetingCompanyAttendeesController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingCompanyAttendeesService meetingCompanyAttendeesService) : base(contextAccessor)
        {
            _context = context;
            _meetingCompanyAttendeesService = meetingCompanyAttendeesService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingCompanyAttendeesDTO model)
        {
            //if (_meetingCompanyAttendeesService.IsCompanyExists(model.Name.Trim(), model.Id))
            //{
            //    return BadRequest(new { isSuccess = false, message = "GetCompanySelectList already exists." });
            //}
            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _meetingCompanyAttendeesService.AddMeetingCompanyAttendees(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }


        [Route("GetByMeetingId/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetByMeetingId(Guid meetingId)
        {
            var list = await _meetingCompanyAttendeesService.GetCompanyAttendeesByMeetingId(meetingId);
            //if (data == null)
            //{
            //    return BadRequest(new { isSuccess = false, message = "No record found." });
            //}
            return Ok(new { isSuccess = true, list = list });
        }
        #endregion
    }
}
