using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class CurrencyDTO
    {
        [Column(TypeName = "nchar(3)")]
        [DataMember(EmitDefaultValue = false)]
        public string Code { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? CreatedById { get; set; }
        [Column(TypeName = "smalldatetime")]
        [DataMember(EmitDefaultValue = false)]
        public DateTime? UpdatedOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? UpdatedById { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        //public Guid? CompanyId { get; set; }
    }
}
