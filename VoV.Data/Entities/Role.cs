using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = null!;
    }
}
