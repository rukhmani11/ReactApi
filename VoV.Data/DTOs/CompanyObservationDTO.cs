//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace VoV.Data.DTOs
//{
//    [DataContract]
//    public class CompanyObservationDTO : BaseDTO
//    {
    
//        [MaxLength(200)]
//        [DataMember(EmitDefaultValue = false)]
//        public string Name { get; set; } = null!;
//        [MaxLength(2000)]
//        [DataMember(EmitDefaultValue = false)]
//        public string Description { get; set; } = null!;
//        [DataMember(EmitDefaultValue = true)]
//        public int Sequence { get; set; }
//        [DataMember(EmitDefaultValue = false)]
//        public Guid? BusinessSegmentId { get; set; }
    

//        [DataMember(EmitDefaultValue = false)]
//        public BusinessSegmentDTO? businessSegment { get; set; }

//        [DataMember]
//        public bool Active { get; set; }
//        [DataMember(EmitDefaultValue = false)]
//        public Guid CompanyId { get; set; }
//    }
//}
