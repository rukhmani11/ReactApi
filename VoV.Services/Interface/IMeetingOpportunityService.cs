using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IMeetingOpportunityService : IDisposable
    {
        Task<Guid> AddMeetingOpportunity(MeetingOpportunityDTO model);

        Task<Guid?> EditMeetingOpportunity(MeetingOpportunityDTO model);

        Task<List<MeetingOpportunityDTO>> GetMeetingOpportunityByMeetingId(Guid meetingId);

        Task<List<MeetingOpportunityDTO>> GetAllMeetingOpportunity();

        Task<bool> DeleteMeetingOpportunity(Guid id);

        Task<MeetingOpportunityDTO> GetMeetingOpportunityById(Guid id);

        Task<List<MeetingOpportunityDTO>> GetPendingMeetingOpportunityByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId);

        //List<spTestResult> spTest();
    }
}
