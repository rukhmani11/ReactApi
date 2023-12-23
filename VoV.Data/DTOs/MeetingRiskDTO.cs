using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;
using System.Runtime.Serialization;

namespace VoV.Data.DTOs
{
    public class MeetingRiskDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid MeetingId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyRiskId { get; set; }

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Remarks { get; set; } = null!;

        [DataMember]
        public bool ActionRequired { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? ActionDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? AssignedToUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Responsibility { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public DateTime? DeadLine { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string RiskStatus { get; set; } = null!; 

        [DataMember(EmitDefaultValue = false)]
        public DateTime? DateOfClosing { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MeetingDTO? Meeting { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public CompanyRiskDTO? CompanyRisk { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public UserDTO? AssignedToUser { get; set; }
    }
}

