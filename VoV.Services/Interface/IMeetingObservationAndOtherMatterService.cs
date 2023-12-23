using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IMeetingObservationAndOtherMatterService : IDisposable
    {

        Task<Guid> AddMeetingObservationAndOtherMatter(MeetingObservationAndOtherMatterDTO model);

        Task<Guid?> EditMeetingObservationAndOtherMatter(MeetingObservationAndOtherMatterDTO model);

        Task<bool> DeleteMeetingObservationAndOtherMatter(Guid id);

        Task<MeetingObservationAndOtherMatterDTO> GetMeetingObservationAndOtherMatterById(Guid id);

        Task<List<MeetingObservationAndOtherMatterDTO>> GetAllMeetingObservationAndOtherMatter();

        Task<List<MeetingObservationAndOtherMatterDTO>> GetMeetingObservationAndOtherMatterByMeetingId(Guid meetingId);

        Task<List<MeetingObservationAndOtherMatterDTO>> GetPendingMeetingObservationAndOtherMatterByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId);
    }
  
}
