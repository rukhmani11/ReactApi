using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("ClientFinancial")]
    public class ClientFinancialController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientFinancialService _clientFinancialsService;
        #endregion

        #region Constructor
        public ClientFinancialController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientFinancialService ClientFinancialsService) : base(contextAccessor)
        {
            _context = context;
            _clientFinancialsService = ClientFinancialsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientFinancialDTO model)
        {
            if (_clientFinancialsService.IsClientFinancialExists(model.ClientId, model.FinancialYearId , model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Client Financial already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _clientFinancialsService.AddClientFinancial(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientFinancialDTO model)
        {
            if (_clientFinancialsService.IsClientFinancialExists(model.ClientId, model.FinancialYearId, model.  Id))
            {
                return BadRequest(new { isSuccess = false, message = "Client Financial already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientFinancialsService.EditClientFinancial(model);
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
            var res = await _clientFinancialsService.GetAllClientFinancials();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{clientFinancialId}")]
        public async Task<ActionResult> Delete(Guid clientFinancialId)
        {
            var isDeleted = await _clientFinancialsService.DeleteClientFinancial(clientFinancialId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{clientFinancialId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid clientFinancialId)
        {
            var data = await _clientFinancialsService.GetClientFinancialById(clientFinancialId);
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
            List<SelectListDTO> res = _clientFinancialsService.GetClientFinancialSelectList();
            return Ok(res);
        }

        [Route("GetByClientId/{clientId}")]
        [HttpGet]
        public async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var res = await _clientFinancialsService.GetClientFinancialByclientId(clientId);
            return Ok(new { isSuccess = true, list = res });
        }
        #endregion
    }
}

