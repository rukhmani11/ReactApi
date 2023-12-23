using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class MeetingOpportunityDTO : BaseDTO
    {
        // public MeetingOpportunity() { }
        [DataMember(EmitDefaultValue = false)]
        public Guid MeetingId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyOpportunityId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Remarks { get; set; } = null!;

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember]
        public bool ActionRequired { get; set; }
        [MaxLength(500)]

        [DataMember(EmitDefaultValue = false)]
        public string? ActionDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? AssignedToUserId { get; set; }

        [MaxLength(500)]
        [DataMember(EmitDefaultValue = false)]
        public string Responsibility { get; set; } = null!;


        [Column(TypeName = "date")]
        [DataMember(EmitDefaultValue = false)]
        public DateTime? DeadLine { get; set; }

        [MaxLength(1)]
        [DataMember(EmitDefaultValue = false)]
        public string OpportunityStatus { get; set; } = null!; // Open / Close

        [Column(TypeName = "date")]
        [DataMember(EmitDefaultValue = false)]
        public DateTime? DateOfClosing { get; set; }

        //Navigational Property
        [DataMember(EmitDefaultValue = false)]
        public MeetingDTO? Meeting { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public CompanyOpportunityDTO? CompanyOpportunity { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public UserDTO? AssignedToUser { get; set; }
    }
}
