using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IMeetingRiskService : IDisposable
    {
        Task<Guid> AddMeetingRisk(MeetingRiskDTO model);
        Task<Guid?> EditMeetingRisk(MeetingRiskDTO model);
        Task<bool> DeleteMeetingRisk(Guid id);
        Task<List<MeetingRiskDTO>> GetAllMeetingRisk();
        Task<MeetingRiskDTO> GetMeetingRiskById(Guid id);

        //Task<List<MeetingRiskDTO>> GetMeetingRisksByMeetingId(Guid meetingId);

        Task<List<MeetingRiskDTO>> GetMeetingRisksByMeetingId(Guid MeetingId);

        Task<List<MeetingRiskDTO>> GetPendingMeetingRisksByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId);
        List<spTestResult> spTest();
    }
}
