using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class ClientFinancialFile : BaseEntity
    {
        public Guid ClientFinancialId { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; } = null!;
        public virtual ClientFinancial ClientFinancial { get; set; } = null!;
    }
}
