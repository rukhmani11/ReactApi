using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{

    [DataContract]
    public class CompanyDTO : BaseDTO
    {
        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;
        [MaxLength(2000)]
        [DataMember(EmitDefaultValue = false)]
        public string Address { get; set; } = null!;
        [MaxLength(100)]
        [DataMember(EmitDefaultValue = false)]
        public string? Logo { get; set; }
        [MaxLength(100)]
        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; } = null!;
        [MaxLength(200)]
        [DataMember(EmitDefaultValue = false)]
        public string? Website { get; set; }
        [DataMember]
        public bool ADLoginYn { get; set; }
        [DataMember]
        public bool MobileIronYn { get; set; }
        [DataMember]
        public bool Active { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? ThemeLightHexCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? ThemeDarkHexCode { get; set; }
        
        [DataMember(EmitDefaultValue =false)]
        public string? SiteAdminName { get; set; }
 
        [DataMember(EmitDefaultValue =false)]
        public string? SiteAdminEmpCode { get; set; }

        [DataMember(EmitDefaultValue =false)]
        public string? SiteAdminUserName { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string? SiteAdminMobile { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? SiteAdminEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid SiteAdminRoleId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? SiteAdminRoleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? SiteAdminPassword { get; set; }

        //public SiteAdminUserDTO? SiteAdmin { get; set; }

        //public class SiteAdminUserDTO
        //{
        //    [DataMember(EmitDefaultValue = false)]
        //    public int Id { get; set; }

        //    [DataMember(EmitDefaultValue = false)]
        //    [MaxLength(200)]
        //    public string EmpCode { get; set; } = null!;

        //    [DataMember(EmitDefaultValue = false)]
        //    [MaxLength(50)]
        //    public string UserName { get; set; } = null!;

        //    [DataMember(EmitDefaultValue = false)]
        //    [MaxLength(100)]
        //    public string Name { get; set; } = null!;

        //    [DataMember(EmitDefaultValue = false)]
        //    [MaxLength(15)]
        //    public string Mobile { get; set; } = null!;

        //    [DataMember(EmitDefaultValue = false)]
        //    [MaxLength(100)]
        //    public string Email { get; set; } = null!;

        //    [DataMember(EmitDefaultValue = false)]
        //    public Guid RoleId { get; set; }

        //    [DataMember(EmitDefaultValue = false)]
        //    public string? RoleName { get; set; }

        //}

    }
}
