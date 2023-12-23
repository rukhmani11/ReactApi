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
    public class ClientGroupDTO : BaseDTO
    {
        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string GroupName { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(50)]
        public string GroupCIFNo { get; set; } = null!;

        [DataMember]
        public bool Active { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyId { get; set; }
    }
}
