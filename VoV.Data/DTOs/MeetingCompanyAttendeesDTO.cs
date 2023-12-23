using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class MeetingCompanyAttendeesDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid MeetingId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Remarks { get; set; } = null!;

        //Navigational Property
        [DataMember(EmitDefaultValue = false)]
        public MeetingDTO Meeting { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public UserDTO CompanyUser { get; set; } = null!;

    }
}
