using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class StandardOpportunityDTO : BaseDTO
    {
        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(2000)]
        public string Description { get; set; } = null!;
        [DataMember(EmitDefaultValue = true)]
        public int Sequence { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid? BusinessSegmentId { get; set; }
        [DataMember]
        public bool Active { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BusinessSegmentDTO? BusinessSegment { get; set; } 
    }
}
