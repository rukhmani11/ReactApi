using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class EmployeeDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Empcode { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
        public string? Userid { get; set; }
    }
}
