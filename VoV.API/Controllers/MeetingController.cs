using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;
using static VoV.Data.DTOs.SearchMeetingDTO;

namespace VoV.API.Controllers
{
    [Route("Meeting")]
    public class MeetingController : BaseApiController

    {
        #region Properties
        private readonly VoVDbContext _context;
        private IMeetingService _meetingService;
        #endregion

        #region Constructor
        public MeetingController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IMeetingService meetingService) : base(contextAccessor)
        {
            _context = context;
            _meetingService = meetingService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingDTO model)
        {
            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _meetingService.AddMeeting(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Data.", id = id });
        }


        [Route("GetByStatus/{MeetingStatus}")]
        [HttpGet]
        public async Task<IActionResult> GetByStatus(string MeetingStatus)
        {
            var res = await _meetingService.GetMeetingsByStatus( MeetingStatus);
            return Ok(new { isSuccess = true, list = res });
        }


        [Route("SearchMeetings")]
        [HttpPost]
        public async Task<IActionResult> SearchMeetings(SearchMeetingDTO model)
        {
            var res = await _meetingService.SearchMeetings(model);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(MeetingDTO model)
        {
           
            model.UpdatedById = currentUser.Id;
            Guid? id = await _meetingService.EditMeeting(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated Record." });
        }

        [Route("GetById/{meetingId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid meetingId)
        {
            var data = await _meetingService.GetMeetingById(meetingId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        //public async Task<List<MeetingRiskDTO>> GetAllMeetingRisk()
        //{
        //    using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
        //    {
        //        var res = await _dbContext.MeetingRisks.Include(x => x.Meeting).Include(x => x.CompanyRisk).Include(x => x.AssignedToUser)
        //            .Where(x => x.RiskStatus == "P").ToListAsync();
        //        var result = res.Select(x => new MeetingRiskDTO()
        //        {
        //            Id = x.Id,
        //            MeetingId = x.MeetingId,
        //            CompanyRiskId = x.CompanyRiskId,
        //            AssignedToUserId = x.AssignedToUserId,
        //            IsCritical = x.IsCritical,
        //            Remarks = x.Remarks,
        //            Responsibility = x.Responsibility,
        //            DeadLine = x.DeadLine,
        //            Meeting = x.Meeting == null ? null : new MeetingDTO()
        //            {
        //                MeetingNo = x.Meeting.MeetingNo,
        //                ScheduledOn = x.Meeting.ScheduledOn,
        //                MeetingPurpose = x.Meeting.MeetingPurpose,
        //                Agenda = x.Meeting.Agenda
        //            },
        //            CompanyRisk = x.CompanyRisk == null ? null : new CompanyRiskDTO()
        //            {
        //                Name = x.CompanyRisk.Name,
        //            },
        //            AssignedToUser = x.AssignedToUser == null ? null : new UserDTO()
        //            {
        //                UserName = x.AssignedToUser.UserName,
        //            },
        //        }).ToList();
        //        return result;
        //    }
        //}
        [Route("GetByClientIdOrClientBusinessUnitId")]
        [HttpGet]
        public async Task<IActionResult> GetByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? businessUnitId)
        {
            var res = await _meetingService.GetMeetingsByClientIdOrClientBusinessUnitId(clientId, businessUnitId);
            return Ok(new { isSuccess = true, list = res });

        }
        [HttpDelete("{meetingId}")]
        public async Task<ActionResult> Delete(Guid meetingId)
        {
            var isDeleted = await _meetingService.Deletemeeting(meetingId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });

        }
        [Route("MeetingCancellation")]
        [HttpPut]
        public async Task<IActionResult> MeetingCancellation(RemarkMeetingDTO model)
        {

            model.UpdatedById = currentUser.Id;
            Guid? Id = await _meetingService.MeetingCancellation(model);
            if (Id == null || Id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated Record." });
        }

        [Route("GetSelectListUser/{ReportingToUserId}")]
        [HttpGet]
        public IActionResult GetuserSelectList(Guid ReportingToUserId)
        {
            List<SelectListDTO> res = _meetingService.GetuserSelectList(ReportingToUserId);
            return Ok(res);
        }
        //[Route("GetSelectListUser")]
        //[HttpPost]
        //public IActionResult GetuserSelectList(SearchMeetingDTO model)
        //{
        //    List<SelectListDTO> res = _meetingService.GetuserSelectList(model);
        //    return Ok(res);
        //}
        #endregion
    }
}
