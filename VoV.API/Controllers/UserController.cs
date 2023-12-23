using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("User")]
    public class UsersController : BaseApiController
    {
        #region Properties
        IUserService _usersService;
        #endregion

        #region Constructor
        public UsersController(IHttpContextAccessor contextAccessor,
           IUserService usersService

           ) : base(contextAccessor)
        {
            this._usersService = usersService;
        }
        #endregion

        #region Methods
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserDTO model)
        {
            var tupleResult = await _usersService.Register(model,currentUser.RoleName);
            bool isSuccess = tupleResult.Item1;
            string message = tupleResult.Item2;
            if (!isSuccess)
            {
                return BadRequest(new { isSuccess = isSuccess, message = message });
            }
            else
            {
                return Ok(new { isSuccess = isSuccess, message = "User registered successfully." });
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if(!_usersService.ValidateBasicAuthHeader(authHeader))
            {
                return BadRequest(new { isSuccess = false, message = "Incorrect Authorization Header." });
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                //var k = identity.FindFirst("ClaimName").Value;
            }

            var result = await _usersService.Login(model);
            bool isSuccess = result.Item1;
            string message = result.Item2;
            UserDTO user = result.Item3;

            if (!isSuccess)
                return BadRequest(new { isSuccess = false, status = "Failure", message = message });
            //else
            //{
            //    string baseURL = $"{this.Request.Scheme}://{this.Request.Host}";
            //    user.ProfileUrl = _commonService.GetVirtualFilePath(baseURL, user.Id, FileUploadEnum.ProfileImage.ToString());
            //    user.Address = user.Address + (!string.IsNullOrEmpty(user.City) ? ("-" + user.City) : null);
            //}
            return Ok(new { isSuccess = true, status = "Success", row = result.Item3 });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _list = await _usersService.GetAllUser();
            return Ok(new { isSuccess = true, list = _list });
        }

        [Route("GetByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var _list = await _usersService.GetUsersByCompanyId(companyId ,currentUser.RoleName);
            return Ok(new { isSuccess = true, list = _list });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var _user = await _usersService.GetUserById(id);
            if (_user == null)
                return BadRequest(new { isSuccess = false, message = "No record found." });
            return Ok(new { isSuccess = true, row = _user });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(UserDTO model)
        {
            model.UpdatedById = currentUser.Id;
            Tuple<bool, string> response = await _usersService.UpdateUser(model);

            if (!response.Item1)
            {
                return BadRequest(new { isSuccess = false, message = response.Item2 });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var isDeleted = await _usersService.DeleteUser(id);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "user not found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetReportingUserSelectList")]
        [HttpGet]
        public async Task<IActionResult> GetReportingUserSelectList(Guid? Id)
        {
            var list = await _usersService.GetReportingUserSelectList(Id);
            return Ok(list);
        }

        [Route("IsUserNameExists")]
        [HttpGet]
        public IActionResult IsUserNameExists(string userName, Guid? id)
        {
            bool isExists = _usersService.IsUserNameExists(userName, id);
            return Ok(new { isSuccess = true, isExists = _usersService.IsUserNameExists(userName, id) });
        }

        [AllowAnonymous]
        [Route("GetAppSetting")]
        [HttpGet]
        public async Task<IActionResult> GetAppSetting()
        {
            var data = await _usersService.GetAppSetting();
            return Ok(new { isSuccess = true, row = data });
        }

        [Route("ChangePassword")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            model.UpdatedById = currentUser.Id;
            Tuple<bool, string> response = await _usersService.ChangePassword(model);

            if (!response.Item1)
            {
                return BadRequest(new { isSuccess = false, message = response.Item2 });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated Password." });
        }

        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _usersService.GetUsersSelectList();
            return Ok(res);
        }
        [Route("EditUser")]
        [HttpPut]
        public async Task<IActionResult> EditUser(UserDTO model)
        {
            model.UpdatedById = currentUser.Id;
            var id = await _usersService.EditUser(model);
            if (id == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = id, message = "Successfully updated record." });
        }

        #endregion
    }
}
