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
    public class RoleDTO : BaseDTO
    
    {
        [Column(TypeName = "nvarchar(50)")]
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

    }
}
