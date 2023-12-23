using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("ClientEmployee")]

    public class ClientEmployeeController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientEmployeeService _clientEmployeesService;
        #endregion

        #region Constructor
        public ClientEmployeeController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientEmployeeService clientEmployeeService) : base(contextAccessor)
        {
            _context = context;
            _clientEmployeesService = clientEmployeeService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientEmployeeDTO model)
        {
            if (_clientEmployeesService.IsClientEmployeeExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientEmployee already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _clientEmployeesService.AddClientEmployee (model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientEmployeeDTO model)
        {
            if (_clientEmployeesService.IsClientEmployeeExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientEmployee already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientEmployeesService.EditClientEmployee(model);
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
            var res = await _clientEmployeesService.GetAllClientEmployee();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{clientEmployeeId}")]
        public async Task<ActionResult> Delete(Guid clientEmployeeId)
        {
            var isDeleted = await _clientEmployeesService.DeleteClientEmployee(clientEmployeeId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{clientEmployeeId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid clientEmployeeId)
        {
            var data = await _clientEmployeesService.GetClientEmployeeById(clientEmployeeId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _clientEmployeesService.GetClientEmployeeSelectList();
            return Ok(res);
        }

        [Route("GetSelectClientEmployeeByclientId/{clientId}")]
        [HttpGet]
        public IActionResult GetSelectClientEmployeeByclientId(Guid clientId)
        {
            List<SelectListDTO> res = _clientEmployeesService.GetSelectClientEmployeeByclientId(clientId);
            return Ok(res);
        }
        [Route("GetClientEmployeeByclientId/{clientId}")]
        [HttpGet]
        public async Task<IActionResult> GetClientEmployeeByclientId(Guid clientId)
        {
            var res = await _clientEmployeesService.GetClientEmployeeByclientId(clientId);
            return Ok(new { isSuccess = true, list = res });
        }
        #endregion
    }
}