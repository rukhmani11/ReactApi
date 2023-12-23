using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("AccountType")]
    public class AccountTypeController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IAccountTypeService _accountTypeService;
        #endregion

        #region Constructor
        public AccountTypeController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IAccountTypeService accountTypeService) : base(contextAccessor)
        {
            _context = context;
            _accountTypeService = accountTypeService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(AccountTypeDTO model)
        {
            if (_accountTypeService.IsAccountTypeExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "AccountType already exists." });
            }
            model.CreatedById = currentUser.Id;
            Guid id = await _accountTypeService.AddAccountType(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(AccountTypeDTO model)
        {
            if (_accountTypeService.IsAccountTypeExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "AccountType already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _accountTypeService.EditAccountType(model);
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
            var res = await _accountTypeService.GetAllAccountTypes();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{accountTypeId}")]
        public async Task<ActionResult> Delete(Guid accountTypeId)
        {
            var isDeleted = await _accountTypeService.DeleteAccountType(accountTypeId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
        }

        [Route("GetById/{accountTypeId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid accountTypeId)
        {
            var data = await _accountTypeService.GetAccountTypeById(accountTypeId);
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
            List<SelectListDTO> res = _accountTypeService.GetAccountTypeSelectList();
            return Ok(res);
        }
        #endregion
    }
}
