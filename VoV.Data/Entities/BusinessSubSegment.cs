using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class BusinessSubSegment : BaseEntity
    {
        public BusinessSubSegment()
        {
            this.ClientBusinessUnits = new HashSet<ClientBusinessUnit>();
        }
        public Guid BusinessSegmentId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public bool Active { get; set; }

        //Navigational Properties
        public virtual BusinessSegment BusinessSegment { get; set; } = null!;
        public virtual ICollection<ClientBusinessUnit> ClientBusinessUnits { get; set; }
    }
}
