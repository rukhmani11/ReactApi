//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VoV.Data.Entities
//{
//    public class StandardObservation : BaseEntity
//    {
//        public StandardObservation()
//        {
//            this.Active = true;
//        }
//        [MaxLength(200)]
//        public string Name { get; set; } = null!;
//        [MaxLength(2000)]
//        public string Description { get; set; } = null!;
//        public int Sequence { get; set; }
//        public Guid? BusinessSegmentId { get; set; }
//        public bool Active { get; set; }
//        public virtual BusinessSegment? BusinessSegment { get; set; }
//    }
//}
