using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("ClientAccount")]
    public class ClientAccountController : BaseApiController
    {
        #region Properties
        private IClientAccountService _clientAccountService;
        #endregion

        #region Constructor
        public ClientAccountController(IHttpContextAccessor contextAccessor,
                                IClientAccountService clientAccountService) : base(contextAccessor)
        {
            _clientAccountService = clientAccountService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientAccountDTO model)
        {
            if (_clientAccountService.IsClientAccountExists(model.AccountNo.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientAccount already exists." });
            }
            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _clientAccountService.AddClientAccount(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientAccountDTO model)
        {
            if (_clientAccountService.IsClientAccountExists(model.AccountNo.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientAccount already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientAccountService.EditClientAccount(model);
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
            var res = await _clientAccountService.GetAllClientAccount();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{ClientAccountId}")]
        public async Task<ActionResult> Delete(Guid ClientAccountId)
        {
            var isDeleted = await _clientAccountService.DeleteClientAccount(ClientAccountId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{ClientAccountId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid ClientAccountId)
        {
            var data = await _clientAccountService.GetClientAccountById(ClientAccountId);
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
            List<SelectListDTO> res = _clientAccountService.GetClientAccountSelectList();
            return Ok(res);
        }

        [Route("GetClientAccountByClientId/{clientId}")]
        [HttpGet]
        public async Task<IActionResult> GetClientAccountByClientId(Guid clientId)
        {
            var res = await _clientAccountService.GetClientAccountByClientId(clientId);
            return Ok(new { isSuccess = true, list = res });
        }


        #endregion
    }
}