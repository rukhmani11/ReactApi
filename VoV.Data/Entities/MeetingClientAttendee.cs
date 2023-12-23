using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class MeetingClientAttendee : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public Guid ClientEmployeeId { get; set; }
        public string? Remarks { get; set; }

        //Navigational Property
        public Meeting Meeting { get; set; } = null!;
        public ClientEmployee ClientEmployee { get; set; } = null!;
    }
}
