//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace VoV.Data.Entities
//{
//    public class MeetingOther : BaseEntity
//    {
//        public MeetingOther()
//        {

//        }
//        public Guid MeetingId { get; set; }

//        public string Remarks { get; set; } = null!;
//        public bool IsCritical { get; set; }

//        [MaxLength(500)]
//        public string ActionRequired { get; set; } = null!;

//        public Guid? AssignedToUserId { get; set; }

//        [MaxLength(500)]
//        public string Responsibility { get; set; } = null!;

//        [Column(TypeName = "date")]
//        public DateTime? DeadLine { get; set; }

//        [MaxLength(1)]
//        public string OtherStatus { get; set; } = null!; // Open / Close

//        [Column(TypeName = "date")]
//        public DateTime? DateOfClosing { get; set; }

//        //Navigational Property
//        public Meeting Meeting { get; set; } = null!;

//        public User? AssignedToUser { get; set; }
//    }
//}
