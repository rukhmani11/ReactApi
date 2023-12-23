using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VoV.Data.DTOs;

namespace VoV.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        #region Properties
        public CurrentUserDTO? currentUser;
        #endregion
        public BaseApiController(IHttpContextAccessor contextAccessor)
        {
            var authenticatedUser = contextAccessor.HttpContext?.User;
            // var claimEmail = System.Security.Claims.ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
            //string userId = User.Claims.First(c => c.Type ==  "UserID").Value;
            if (authenticatedUser != null)
            {
                var IdentityClaims = (ClaimsIdentity)authenticatedUser.Identity;
                //string userId = authenticatedUser.Claims.First(c => c.Type == "UserID").Value;                                
                if (IdentityClaims != null)
                {
                    string strUserID = (IdentityClaims.FindFirst("Id") != null ? IdentityClaims.FindFirst("Id").Value : null);
                    //string strRoleIDs = (IdentityClaims.FindFirst("RoleIDs") != null ? IdentityClaims.FindFirst("RoleIDs").Value : null);
                    //strRoleIDs = string.IsNullOrEmpty(strRoleIDs) ? null : strRoleIDs;
                    if (!string.IsNullOrEmpty(strUserID))
                    {
                        currentUser = new CurrentUserDTO()
                        {
                            Id = new Guid(strUserID),
                            UserName = (IdentityClaims.FindFirst("UserName") != null ? IdentityClaims.FindFirst("UserName").Value : null),
                            Email = (IdentityClaims.FindFirst(ClaimTypes.Email) != null ? IdentityClaims.FindFirst(ClaimTypes.Email).Value : null),
                            //RoleNames = (IdentityClaims.FindAll(ClaimTypes.Role) != null ? IdentityClaims.FindAll(ClaimTypes.Role)?.Select(x => x.Value).ToList() : new List<string>()),
                            RoleName = (IdentityClaims.FindAll(ClaimTypes.Role) != null ? IdentityClaims.FindFirst(ClaimTypes.Role)?.Value : null),
                        };
                    }
                }//Ends IdentityClaims
            }//Ends (ClaimsPrincipal)User
        }
    }
}
