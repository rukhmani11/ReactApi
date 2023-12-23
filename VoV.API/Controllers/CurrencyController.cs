using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("Currency")]
    public class CurrencyController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private ICurrencyService _currenciesService;
        #endregion

        #region Constructor
        public CurrencyController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                ICurrencyService CurrencysService) : base(contextAccessor)
        {
            _context = context;
            _currenciesService = CurrencysService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(CurrencyDTO model)
        {
            if (_currenciesService.IsCurrencyExists(model.Name.Trim(), model.Code))
            {
                return BadRequest(new { isSuccess = false, message = "Currency already exists." });
            }
            model.CreatedById = currentUser.Id;
           // model.CreatedById = string.Empty;
            string currencyCode = await _currenciesService.AddCurrency(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", currencyCode = currencyCode });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(CurrencyDTO model)
        {
            if (_currenciesService.IsCurrencyExists(model.Name.Trim(), model.Code))
            {
                return BadRequest(new { isSuccess = false, message = "Currency already exists." });
            }
            model.UpdatedById = currentUser.Id;
            string? id = await _currenciesService.EditCurrency(model);
            if (id == null || id == string.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _currenciesService.GetAllCurrencys();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var isDeleted = await _currenciesService.DeleteCurrency(id);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{Code}")]
        [HttpGet]
        public async Task<IActionResult> GetById(string Code)
        {
            var data = await _currenciesService.GetCurrencyBycurrencyCode(Code);
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
            List<SelectListDTO> res = _currenciesService.GetCurrencySelectList();
            return Ok(res);
        }
        #endregion
    }
}

