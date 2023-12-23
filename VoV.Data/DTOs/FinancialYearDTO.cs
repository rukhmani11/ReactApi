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
    public class FinancialYearDTO : BaseDTO
    {
        [Column(TypeName = "date")]
        [DataMember(EmitDefaultValue = false)]
        public DateTime FromDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(15)]
        public string Abbr { get; set; } = null!;
    }
}
