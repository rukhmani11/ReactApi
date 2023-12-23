using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Meeting : BaseEntity
    {
        public Meeting()
        {
            this.MeetingObservations = new HashSet<MeetingObservationAndOtherMatter>();
            this.MeetingOpportunities = new HashSet<MeetingOpportunity>();
            // this.MeetingOthers = new HashSet<MeetingOther>();
            this.MeetingRisks = new HashSet<MeetingRisk>();
            //this.MeetingOtherMatters = new HashSet<MeetingOtherMatter>();
            this.MeetingClientAttendees = new HashSet<MeetingClientAttendee>();
            this.MeetingCompanyAttendees = new HashSet<MeetingCompanyAttendee>();
        }
        public Guid CompanyId { get; set; }
        public Guid ClientId { get; set; }
        public Guid CompanyUserId { get; set; }
        public DateTime ScheduledOn { get; set; }
        public int SrNo { get; set; }

        [MaxLength(100)]
        public string MeetingNo { get; set; } = null!;
        public Guid ClientEmployeeId { get; set; }
        public Guid ClientBusinessUnitId { get; set; }

        [MaxLength(500)]
        public string MeetingPurpose { get; set; } = null!;
        public string Agenda { get; set; } = null!;
        public Int16 MeetingStatusId { get; set; }

        [MaxLength(15)]
        [Column(TypeName = "nvarchar")]
        public string MeetingStatus { get; private set; } = null!;
        public DateTime? VisitedOn { get; set; }
        public string? VisitSummary { get; set; }
        public Guid? VisitedCompanyUserId { get; set; }
        public Guid? VisitedClientEmployeeId { get; set; }
        public Guid? VisitedClientBusinessUnitId { get; set; }
        
        [MaxLength(500)]
        [Column(TypeName = "nvarchar")]
        public string? CancelRemark { get; set; }
        //Navigational properties
        public Company Company { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public User CompanyUser { get; set; } = null!;
        public ClientEmployee ClientEmployee { get; set; } = null!;
        public ClientBusinessUnit ClientBusinessUnit { get; set; } = null!;
        public User? VisitedCompanyUser { get; set; }
        public ClientEmployee? VisitedClientEmployee { get; set; }
        public ClientBusinessUnit? VisitedClientBusinessUnit { get; set; } 

        public virtual ICollection<MeetingObservationAndOtherMatter> MeetingObservations { get; set; }
        public virtual ICollection<MeetingOpportunity> MeetingOpportunities { get; set; }
        //public virtual ICollection<MeetingOther> MeetingOthers { get; set; }
        public virtual ICollection<MeetingRisk> MeetingRisks { get; set; }
        //public virtual ICollection<MeetingOtherMatter> MeetingOtherMatters { get; set; }
        public virtual ICollection<MeetingClientAttendee> MeetingClientAttendees { get; set; }
        public virtual ICollection<MeetingCompanyAttendee> MeetingCompanyAttendees { get; set; }
    }
}
