using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;
using System.Runtime.Serialization;

namespace VoV.Data.DTOs
{

    [DataContract]
    public class UserDTO : BaseDTO
    {
        [DataMember(EmitDefaultValue = false)]
        [MaxLength(200)]
        public string? EmpCode { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(15)]
        public string Mobile { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public Guid RoleId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? RoleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? CompanyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? LocationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? DesignationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? ReportingToUserId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? BusinessUnitId { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? JwtTokenExpiresOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? JwtTokenExpiryDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string? JwtToken { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string? Password { get; set; }

        //public virtual Role Role { get; set; } = null!;
        [DataMember(EmitDefaultValue = false)]
        public virtual CompanyDTO? Company { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual LocationDTO? Location { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public virtual MeetingDTO? Meeting { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public virtual DesignationDTO? Designation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual UserDTO? ReportingToUser { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual BusinessUnitDTO? BusinessUnit { get;set; }
    }

    [DataContract]
    public class CurrentUserDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string UserName { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string RoleName { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string? JwtToken { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? JwtTokenExpiresOn { get; set; }
    }

    [DataContract]
    public class LoginDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string UserName { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public string Password { get; set; } = null!;
    }

    [DataContract]
    public class ChangePasswordDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Password { get; set; } = null!;

        [DataMember(EmitDefaultValue = false)]
        public Guid UpdatedById { get; set; }
    }
public class ResetPasswordDTO
{
    public string? UserId { get; set; }
    public string Password { get; set; } = null!;

  

}
}

