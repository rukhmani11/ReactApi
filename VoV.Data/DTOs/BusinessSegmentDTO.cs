using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class BusinessSegmentDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]          
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string? Code { get; set; } 
        [DataMember]
        public bool Active { get; set; }
    }
}
