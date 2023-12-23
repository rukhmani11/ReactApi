using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{

    [DataContract]
    public class BusinessUnitDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;
        [DataMember]

        public bool Active { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string? Code { get; set; }
        [DataMember(EmitDefaultValue = false)]

        public Guid CompanyId { get; set; }
        [DataMember(EmitDefaultValue = false)]

        public Guid? ParentId { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public BusinessUnitDTO? Parent { get; set; }
    }
}
