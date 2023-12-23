using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    public class DocumentDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public long FileSize { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FileName { get; set; }
    }
}
