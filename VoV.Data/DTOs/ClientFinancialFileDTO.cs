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
    public class ClientFinancialFileDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid ClientFinancialId { get; set; }


        [DataMember(EmitDefaultValue = false)]
        [MaxLength(200)]
        public string FileName { get; set; } = null!;
    }
}
