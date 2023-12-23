using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class AppSettingDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CompanyName { get; set; } = null!;
        
        [DataMember(EmitDefaultValue = false)]
        public string ThemeDarkHexCode { get; set; } = null!;
        
        [DataMember(EmitDefaultValue = false)]
        public string ThemeLightHexCode { get; set; } = null!;
    }
}
