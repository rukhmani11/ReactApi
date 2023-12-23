using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;
using VoV.Data.Entities;

namespace VoV.Services.Interface
{
    public interface IUserService : IDisposable
    {
        bool ValidateBasicAuthHeader(string authHeader);
        Task<Tuple<bool, string>> Register(UserDTO model,string currentUser);
        Task<Tuple<bool, string, UserDTO?>> Login(LoginDTO model);
        Task<Tuple<bool, string>> UpdateUser(UserDTO model);
        Task<bool> DeleteUser(Guid id);
        Task<UserDTO> GetUserById(Guid id);
        Task<List<SelectListDTO>> GetUserSelectList();
        Task<List<SelectListDTO>> GetReportingUserSelectList(Guid? Id);
        Task<List<UserDTO>> GetAllUser();
        bool IsUserNameExists(string userName, Guid? id);
        bool IsEmailExists(string email, Guid? id);
        bool IsMobileNoExists(string mobile, Guid? id);
        Task<AppSettingDTO> GetAppSetting();
        Task<Tuple<bool, string>> ChangePassword(ChangePasswordDTO model);
        Task<List<UserDTO>> GetUsersByCompanyId(Guid companyId, string currentUserRoles);

        Task<Guid?> EditUser(UserDTO model);
        List<SelectListDTO> GetUsersSelectList();
    }
}
