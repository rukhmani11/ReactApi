using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VoV.Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.ClientBusinessUnits = new HashSet<ClientBusinessUnit>();
            this.InverseReportingToUser = new HashSet<User>();
            this.MeetingsCompanyUser = new HashSet<Meeting>();
            this.MeetingObservations = new HashSet<MeetingObservationAndOtherMatter>();
            this.MeetingOpportunites = new HashSet<MeetingOpportunity>();
            // this.MeetingOthers = new HashSet<MeetingOther>();
            this.MeetingRisks = new HashSet<MeetingRisk>();
            this.MeetingCompanyAttendees = new HashSet<MeetingCompanyAttendee>();
            this.MeetingsVisitedCompanyUser = new HashSet<Meeting>();
        }
        [MaxLength(200)]
        public string? EmpCode { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        [MaxLength(255)]
        public string Password { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(15)]
        public string Mobile { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        public Guid RoleId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? ReportingToUserId { get; set; }
        public Guid? BusinessUnitId { get; set; }
        public bool Active { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? JwtTokenExpiresOn { get; set; }
        public string? JwtToken { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual Company? Company { get; set; }
        public virtual Location? Location { get; set; }
        public virtual Designation? Designation { get; set; }
        public virtual User? ReportingToUser { get; set; }
        public virtual BusinessUnit? BusinessUnit { get; set; }
        public virtual ICollection<ClientBusinessUnit> ClientBusinessUnits { get; set; }
        public virtual ICollection<User> InverseReportingToUser { get; set; }
        public virtual ICollection<Meeting> MeetingsCompanyUser { get; set; }
        public virtual ICollection<Meeting> MeetingsVisitedCompanyUser { get; set; }
        public virtual ICollection<MeetingObservationAndOtherMatter> MeetingObservations { get; set; }
        public virtual ICollection<MeetingOpportunity> MeetingOpportunites { get; set; }
        //public virtual ICollection<MeetingOther> MeetingOthers { get; set; }
        public virtual ICollection<MeetingRisk> MeetingRisks { get; set; }
        public virtual ICollection<MeetingCompanyAttendee> MeetingCompanyAttendees { get; set; }
    }
}
