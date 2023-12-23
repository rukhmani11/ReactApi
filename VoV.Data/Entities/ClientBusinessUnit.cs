using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class ClientBusinessUnit : BaseEntity
    {
        public ClientBusinessUnit()
        {
            this.ClientEmployees = new HashSet<ClientEmployee>();
            this.MeetingsClientBusinessUnit = new HashSet<Meeting>();
            this.MeetingsVisitedClientBusinessUnit = new HashSet<Meeting>();
        }
        public Guid ClientId { get; set; }
        public Guid BusinessSegmentId { get; set; }
        [MaxLength(300)]
        public string Name { get; set; } = null!;
        public Guid RoUserId { get; set; }
        public Guid? BusinessSubSegmentId { get; set; }

        public virtual BusinessSegment BusinessSegment { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual User RoUser { get; set; } = null!;
        public virtual BusinessSubSegment? BusinessSubSegment { get; set; } 
        public virtual ICollection<ClientEmployee> ClientEmployees { get; set; }
        public virtual ICollection<Meeting> MeetingsClientBusinessUnit { get; set; }
        public virtual ICollection<Meeting> MeetingsVisitedClientBusinessUnit { get; set; }
    }
}
