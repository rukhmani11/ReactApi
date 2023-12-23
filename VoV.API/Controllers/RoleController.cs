using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("Role")]
    public class RoleController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IRoleService _roleService;
        #endregion

        #region Constructor
        public RoleController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IRoleService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _roleService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(RoleDTO model)
        {
            if (_roleService.IsRoleExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Role already exists." });
            }
            //model.CreatedById = currentUser.Id;
            model.CreatedById = Guid.Empty;
            Guid id = await _roleService.AddRole(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(RoleDTO model)
        {
            if (_roleService.IsRoleExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Role already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _roleService.EditRole(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _roleService.GetAllRoles();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult> Delete(Guid roleId)
        {
            var isDeleted = await _roleService.DeleteRole(roleId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{roleId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid roleId)
        {
            var data = await _roleService.GetRoleById(roleId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, row = data });
        }

        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _roleService.GetRoleSelectList(currentUser.RoleName);
            return Ok(res);
        }
        #endregion
    }
}

