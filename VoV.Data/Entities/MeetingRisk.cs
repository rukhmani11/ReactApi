using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class MeetingRisk : BaseEntity
    {
        public MeetingRisk()
        {

        }

        public Guid MeetingId { get; set; }
        public Guid CompanyRiskId { get; set; }
        public bool IsCritical { get; set; }
        public string Remarks { get; set; } = null!;

        public bool ActionRequired { get; set; }
        [MaxLength(500)]
        public string? ActionDetails { get; set; }

        public Guid? AssignedToUserId { get; set; }

        [MaxLength(500)]
        public string Responsibility { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime? DeadLine { get; set; }

        [MaxLength(1)]
        public string RiskStatus { get; set; } = null!; // Open / Close

        [Column(TypeName ="date")]
        public DateTime? DateOfClosing { get; set; }

        //Navigational Property
        public Meeting Meeting { get; set; } = null!;
        public CompanyRisk CompanyRisk { get; set; } = null!;
        public User? AssignedToUser { get; set; }
    }
}
