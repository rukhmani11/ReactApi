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
    [DataContract]
    public class ClientBusinessUnitDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid BusinessSegmentId { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public Guid RoUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public  BusinessSegmentDTO? BusinessSegment { get; set; } 
        
        [DataMember(EmitDefaultValue = false)]
        public  UserDTO? RoUser { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid BusinessSubSegmentId { get; set; }


        //public virtual Client Client { get; set; } = null!;
    }
}
