using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using VoV.Data.Entities;

namespace VoV.Data.DTOs
{

    [DataContract]
    public class ClientFinancialDTO : BaseDTO
    {
       
        [DataMember(EmitDefaultValue = false)]
        public string CurrencyCode { get; set; } = null!;

        [DataMember]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Turnover { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Profit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid FinancialYearId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid CurrencyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CurrencyDTO? Currency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public FinancialYearDTO? FinancialYear { get; set; }
    }
}
