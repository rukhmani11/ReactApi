using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace VoV.Data.Entities
{
    public class AccountType : BaseEntity
    {
        public AccountType()
        {
            this.Active = true;
            this.ClientAccounts = new HashSet<ClientAccount>();
        }
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }
}
