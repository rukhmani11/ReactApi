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
    public class ClientEmployeeDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientBusinessUnitId { get; set; }
        [MaxLength(200)]

        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;
        [MaxLength(15)]

        [DataMember(EmitDefaultValue = false)]
        public string Mobile { get; set; } = null!;
        [MaxLength(100)]

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; } = null!;
        [MaxLength(200)]

        [DataMember(EmitDefaultValue = false)]
        public string? Department { get; set; }
        [MaxLength(200)]

        [DataMember(EmitDefaultValue = false)]
        public string? Designation { get; set; }
        [MaxLength(200)]

        [DataMember(EmitDefaultValue = false)]
        public string? Location { get; set; } 

        [DataMember(EmitDefaultValue = false)]

        public BusinessUnitDTO? businessUnit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ClientBusinessUnitDTO? clientBusinessUnit { get; set; }

    }
}