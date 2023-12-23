using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class AccountTypeDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember]
        public bool Active { get; set; }
    }
}
