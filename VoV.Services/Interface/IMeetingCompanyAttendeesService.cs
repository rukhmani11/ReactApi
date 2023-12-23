using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IMeetingCompanyAttendeesService : IDisposable
    {
        Task<Guid> AddMeetingCompanyAttendees(MeetingCompanyAttendeesDTO model);

        Task<List<MeetingCompanyAttendeesDTO>> GetCompanyAttendeesByMeetingId(Guid MeetingId);
    }
}   