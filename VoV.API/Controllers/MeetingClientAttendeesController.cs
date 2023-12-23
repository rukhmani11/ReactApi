using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("MeetingClientAttendees")]
    public class MeetingClientAttendeesController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingClientAttendeesService _meetingClientAttendeesService;
        #endregion

        #region Constructor
        public MeetingClientAttendeesController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingClientAttendeesService meetingClientAttendeesService) : base(contextAccessor)
        {
            _context = context;
            _meetingClientAttendeesService = meetingClientAttendeesService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingClientAttendeesDTO model)
        {
            //if (_meetingClientAttendeesService.IsClientExists(model.Name.Trim(), model.Id))
            //{
            //    return BadRequest(new { isSuccess = false, message = "GetClientSelectList already exists." });
            //}
            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _meetingClientAttendeesService.AddMeetingClientAttendees(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("GetByMeetingId/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetByMeetingId(Guid meetingId)
        {
            var list = await _meetingClientAttendeesService.GetClientAttendeesByMeetingId(meetingId);
            //if (data == null)
            //{
            //    return BadRequest(new { isSuccess = false, message = "No record found." });
            //}
            return Ok(new { isSuccess = true, list = list });
        }


        #endregion
    }
}
