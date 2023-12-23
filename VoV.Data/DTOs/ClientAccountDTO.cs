using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class ClientAccountDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string AccountNo { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public DateTime BalanceAsOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal Balance { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid AccountTypeId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AccountTypeDTO? accountType { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string CurrencyCode { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public CurrencyDTO? Currency { get; set; }

    }
}
