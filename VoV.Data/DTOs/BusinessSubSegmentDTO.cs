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

    public class BusinessSubSegmentDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid BusinessSegmentId { get; set; }

        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember]
        public bool Active { get; set; }

        //Navigational Properties
        [DataMember(EmitDefaultValue = false)]
        public  BusinessSegmentDTO? BusinessSegment { get; set; }
    }
}


