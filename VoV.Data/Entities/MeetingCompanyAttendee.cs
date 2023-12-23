using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class MeetingCompanyAttendee : BaseEntity
    {
        public MeetingCompanyAttendee() { }
        public Guid MeetingId { get; set; }
        public Guid CompanyUserId { get; set; }
        public string? Remarks { get; set; }

        //Navigational Property
        public Meeting Meeting { get; set; } = null!;
        public User CompanyUser { get; set; } = null!;
    }
}
