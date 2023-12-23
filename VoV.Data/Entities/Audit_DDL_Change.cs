using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.Entities
{
    public class Audit_DDL_Change
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ObjectId { get; set; }

        //[Column(TypeName = "varchar")]
        public string? ObjectSchema { get; set; }

        //[Column(TypeName = "varchar")]
        public string? ObjectName { get; set; }

        //[Column(TypeName = "varchar")]
        public string? ObjectSQL { get; set; }

        //[Column(TypeName = "varchar")]
        public string? Object_Host_Name { get; set; }

        //[Column(TypeName = "varchar")]
        public string? EventType { get; set; }

        //[Column(TypeName = "varchar")]
        public string? ObjectType { get; set; }

        //[Column(TypeName = "varchar")]
        public string? UserName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Mod_Dt { get; set; }
    }
}
