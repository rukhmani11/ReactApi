using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid Id { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid? CreatedById { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public DateTime? CreatedOn { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Guid? UpdatedById { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public DateTime? UpdatedOn { get; set; }
    }
}
