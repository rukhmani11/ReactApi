using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class ClientEmployee : BaseEntity
    {
        public ClientEmployee()
        {
            this.MeetingsClientEmployee = new HashSet<Meeting>();
            this.MeetingClientAttendees = new HashSet<MeetingClientAttendee>();
            this.MeetingsVisitedClientEmployee = new HashSet<Meeting>();
        }
        public Guid ClientId { get; set; }
        public Guid ClientBusinessUnitId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(15)]
        public string Mobile { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [MaxLength(200)]
        public string Department { get; set; } = null!;
        [MaxLength(200)]
        public string Location { get; set; } = null!;
        [MaxLength(200)]
        public string Designation { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual ClientBusinessUnit ClientBusinessUnit { get; set; } = null!;
        public virtual ICollection<Meeting> MeetingsClientEmployee { get; set; }
        public virtual ICollection<Meeting> MeetingsVisitedClientEmployee { get; set; }
        public virtual ICollection<MeetingClientAttendee> MeetingClientAttendees { get; set; }
    }
}
