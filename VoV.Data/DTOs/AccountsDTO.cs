//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace VoV.Data.DTOs
//{
//    public class UserRegisterModel
//    {
//        public string? UserId { get; set; }
//        [Required]
//        public string FirstName { get; set; }
//        [Required]
//        public string LastName { get; set; }
//        [Required]
//        public string UserName { get; set; }

//        [Required]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        [DataType(DataType.Password)]
//        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
//        public string ConfirmPassword { get; set; }

//        public string? Roles { get; set; }

//        [Required]
//        [MinLength(10, ErrorMessage = "Please Enter Correct PhoneNumber!!")]
//        [DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter Correct Phone Number!!")]
//        public string PhoneNumber { get; set; }
//    }

//    public class UpdateUserModel
//    {
//        [Required]
//        public string UserId { get; set; }
//        [Required]
//        public string FirstName { get; set; }
//        [Required]
//        public string LastName { get; set; }
//        public string Roles { get; set; }

//        [Required]
//        public string Email { get; set; }
//        [Required]
//        [MinLength(10, ErrorMessage = "Please Enter Correct PhoneNumber!!")]
//        [DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter Correct Phone Number!!")]
//        public string PhoneNumber { get; set; }
//    }


//public class AccountDTO
//{
//    public Guid UserId { get; set; }
//    public string Name { get; set; } = null!;
//    public string UserName { get; set; } = null!;
//    public string Email { get; set; } = null!;

//    [DataMember(EmitDefaultValue = false)]
//    public string Role { get; set; } = null!;
//    public string? JwtToken { get; set; }
//    public DateTime? JwtTokenExpiresOn { get; set; }
//}

//    public class ChangePasswordModel
//    {
//        public string UserId { get; set; }
//        public string CurrentPassword { get; set; }
//        public string NewPassword { get; set; }
//    }
//}
