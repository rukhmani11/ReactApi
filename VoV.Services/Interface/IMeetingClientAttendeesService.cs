using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IMeetingClientAttendeesService : IDisposable
    {
        Task<Guid> AddMeetingClientAttendees(MeetingClientAttendeesDTO model);

        Task<List<MeetingClientAttendeesDTO>> GetClientAttendeesByMeetingId(Guid MeetingId);

       

    }
}
