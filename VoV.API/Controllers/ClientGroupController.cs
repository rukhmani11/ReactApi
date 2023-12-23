using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("ClientGroup")]
    public class ClientGroupController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientGroupService _clientGroupsService;
        #endregion

        #region Constructor
        public ClientGroupController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientGroupService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _clientGroupsService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientGroupDTO model)
        {
            if (_clientGroupsService.IsClientGroupExists(model.GroupName.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientGroup already exists." });
            }
            model.CreatedById = currentUser.Id;
           // model.CreatedById = Guid.Empty;
            Guid id = await _clientGroupsService.AddClientGroup(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientGroupDTO model)
        {
            if (_clientGroupsService.IsClientGroupExists(model.GroupName.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientGroup already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientGroupsService.EditClientGroup(model);
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
            var res = await _clientGroupsService.GetAllClientGroups();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{clientGroupId}")]
        public async Task<ActionResult> Delete(Guid clientGroupId)
        {
            var isDeleted = await _clientGroupsService.DeleteClientGroup(clientGroupId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{clientGroupId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid clientGroupId)
        {
            var data = await _clientGroupsService.GetClientGroupById(clientGroupId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("GetSelectListByCompanyId/{companyId}")]
        [HttpGet]
        public IActionResult GetSelectListByCompanyId(Guid companyId)
        {
            List<SelectListDTO> res = _clientGroupsService.GetClientGroupSelectListByCompanyId(companyId);
            return Ok(res);
        }

        [Route("GetClientGroupByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetClientGroupByCompanyId(Guid companyId)
        {
            var res = await _clientGroupsService.GetClientGroupByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });

        }   
            
            #endregion
        }
    }
