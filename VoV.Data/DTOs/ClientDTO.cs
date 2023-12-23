using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class ClientDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string CIFNo { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public Guid CompanyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientGroupId { get; set; }

        [DataMember]

        public byte VisitingFrequencyInMonth { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        //public string? CurrencyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ClientGroupDTO? ClientGroup { get; set; }

    }
        [DataContract]
        public class ClientsTitleDTO
        {
            [DataMember(EmitDefaultValue = false)]
             public Guid? ClientId { get; set; }

            [DataMember(EmitDefaultValue = false)]
            public string? Name { get; set; }

    }
}
