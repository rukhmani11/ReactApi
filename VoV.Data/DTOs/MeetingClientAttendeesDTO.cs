using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class MeetingClientAttendeesDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid MeetingId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientEmployeeId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Remarks { get; set; } = null!;

        //Navigational Property
        [DataMember(EmitDefaultValue = false)]
        public MeetingDTO Meeting { get; set; } = null!;

        
        [DataMember(EmitDefaultValue = false)]
        public ClientEmployeeDTO? ClientEmployee { get; set; }

    }
}
