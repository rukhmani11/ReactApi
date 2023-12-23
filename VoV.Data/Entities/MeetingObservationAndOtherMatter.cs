using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class MeetingObservationAndOtherMatter : BaseEntity
    {
        public MeetingObservationAndOtherMatter()
        {

        }
        public Guid MeetingId { get; set; }
        //public Guid? CompanyObservationId { get; set; }
        public string Remarks { get; set; } = null!;
        public bool IsCritical { get; set; }
        public bool ActionRequired { get; set; }
        [MaxLength(500)]
        public string? ActionDetails { get; set; }
        public Guid? AssignedToUserId { get; set; }

        [MaxLength(500)]
        public string Responsibility { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime? DeadLine { get; set; }

        [MaxLength(1)]
        public string ObservationStatus { get; set; } = null!; // Open / Close

        [MaxLength(2000)]
        public string? CompanyObservation { get; set; }  // Open / Close

        [Column(TypeName = "date")]
        public DateTime? DateOfClosing { get; set; }

        //Navigational Property
        public Meeting Meeting { get; set; } = null!;
        //public CompanyObservation? CompanyObservation { get; set; } = null!;
        public User? AssignedToUser { get; set; }

    }
}
