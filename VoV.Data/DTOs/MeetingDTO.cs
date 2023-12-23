using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class MeetingDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientEmployeeId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ScheduledOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ScheduledEnd { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? CancelRemark { get; set; }

        [DataMember]
        public int SrNo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MeetingNo { get; set; } = null!;

        //[DataMember(EmitDefaultValue = false)]
        //public Guid ClientEmployeeId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientBusinessUnitId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MeetingPurpose { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string Agenda { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string? MeetingStatus { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Int16 MeetingStatusId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? VisitedOn { get; set; }

   

        [DataMember(EmitDefaultValue = false)]
        public string? VisitSummary { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? VisitedCompanyUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? VisitedClientEmployeeId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? VisitedClientBusinessUnitId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CompanyDTO? Company { get; set; } = null!;

        //[DataMember(EmitDefaultValue = false)]
        //public Client Client { get; set; } = null!;

        //[DataMember(EmitDefaultValue = false)]
        //public User CompanyUser { get; set; } = null!;

        //[DataMember(EmitDefaultValue = false)]
        //public ClientEmployee ClientEmployee { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public ClientBusinessUnitDTO? ClientBusinessUnit { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public UserDTO? CompanyUser { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public ClientDTO? Client { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public ClientEmployeeDTO? ClientEmployee { get; set; }
        //        MeetingClientAttendeesIds : List<Guid>
        //MeetingCompanyAttendeesIds

        [DataMember(EmitDefaultValue = false)]
        public List<Guid>? MeetingClientAttendeesIds { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public List<MultiSelectDTO>? SelectedMeetingClientAttendees { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<Guid>? MeetingCompanyAttendeesIds { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public List<MultiSelectDTO>? SelectedMeetingCompanyAttendees { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public UserDTO? VisitedCompanyUser { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public ClientEmployeeDTO? VisitedClientEmployee { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public ClientBusinessUnit? VisitedClientBusinessUnit { get; set; }
    }

    [DataContract]
    public class SearchMeetingDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CompanyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? CompanyUserId { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public Guid? ReportingToUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? MeetingCompanyAttendeesIds { get; set; }


        [DataContract]
        public class RemarkMeetingDTO : BaseEntity
        {
            [DataMember(EmitDefaultValue = false)]
            public Guid MeetingId { get; set; }

            [DataMember(EmitDefaultValue = false)]
            public string? CancelRemark { get; set; }

            [DataMember(EmitDefaultValue = false)]
            public int? MeetingStatusId { get; set; }

        }
    }

}

