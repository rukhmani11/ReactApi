using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;
using static VoV.Data.DTOs.SearchMeetingDTO;

namespace VoV.Services.Interface
{
    public interface IMeetingService : IDisposable
    {
        Task<Guid> AddMeeting(MeetingDTO model);
        Task<List<MeetingDTO>> GetMeetingsByStatus(string MeetingStatus);
        //Task<List<MeetingDTO>> GetMeetingsByCompanyId(Guid CompanyId);
        Task<List<MeetingDTO>> SearchMeetings(SearchMeetingDTO model);
        Task<List<MeetingDTO>> GetMeetingsByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId);
        Task<Guid?> EditMeeting(MeetingDTO model);
        Task<MeetingDTO> GetMeetingById(Guid id);
        Task<bool> Deletemeeting(Guid id);
        Task<Guid?> MeetingCancellation(RemarkMeetingDTO model);

        List<SelectListDTO> GetuserSelectList(Guid ReportingToUserId);

        //List<SelectListDTO> GetuserSelectList(SearchMeetingDTO model);


    }
}
