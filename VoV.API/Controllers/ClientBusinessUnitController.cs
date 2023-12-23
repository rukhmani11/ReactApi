using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("ClientBusinessUnit")]
  
    public class ClientBusinessUnitController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientBusinessUnitService _clientBusinessUnitsService;
        #endregion

        #region Constructor
        public ClientBusinessUnitController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientBusinessUnitService clientBusinessUnitService) : base(contextAccessor)
        {
            _context = context;
            _clientBusinessUnitsService = clientBusinessUnitService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientBusinessUnitDTO model)
        {
            if (_clientBusinessUnitsService.IsClientBusinessUnitExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientBusinessUnit already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _clientBusinessUnitsService.AddClientBusinessUnit(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientBusinessUnitDTO model)
        {
            if (_clientBusinessUnitsService.IsClientBusinessUnitExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientBusinessUnit already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientBusinessUnitsService.EditClientBusinessUnit(model);
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
            var res = await _clientBusinessUnitsService.GetAllClientBusinessUnits();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{clientBusinesId}")]
        public async Task<ActionResult> Delete(Guid clientBusinesId)
        {
            var isDeleted = await _clientBusinessUnitsService.DeleteClientBusinessUnit(clientBusinesId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{clientBusinesId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid clientBusinesId)
        {
            var data = await _clientBusinessUnitsService.GetClientBusinessUnitById(clientBusinesId);
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
            List<SelectListDTO> res = _clientBusinessUnitsService.GetClientBusinessUnitSelectList();
            return Ok(res);
        }


        [Route("GetSelectListByClientId/{clientId}")]
        [HttpGet]
        public IActionResult GetSelectListByClientId(Guid ClientId)
        {
            List<SelectListDTO> res = _clientBusinessUnitsService.GetSelectListByClientId(ClientId);
            return Ok(res);
        }




        [Route("GetClientBusinessUnitByclientId/{clientId}")]
        [HttpGet]
        public async Task<IActionResult> GetClientBusinessUnitByclientId(Guid clientId)
        {
            var res = await _clientBusinessUnitsService.GetClientBusinessUnitByclientId(clientId);
            return Ok(new { isSuccess = true, list = res });
        }
        #endregion
    }
}
