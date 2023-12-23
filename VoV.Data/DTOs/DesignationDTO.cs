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
    public class DesignationDTO : BaseDTO
    {
        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;
        [DataMember(EmitDefaultValue = false)]
        [MaxLength(50)]
        public string Code { get; set; } = null!;
        [DataMember(EmitDefaultValue = false)]
        public bool Active { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyId { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid? ParentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DesignationDTO? Parent { get; set; }

    }
}
