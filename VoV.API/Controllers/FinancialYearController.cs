using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("FinancialYear")]
    public class FinancialYearController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IFinancialYearService _financialYearService;
        #endregion

        #region Constructor
        public FinancialYearController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IFinancialYearService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _financialYearService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(FinancialYearDTO model)
        {
            if (_financialYearService.IsFinancialYearExists(model.Abbr.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "FinancialYear already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _financialYearService.AddFinancialYear(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(FinancialYearDTO model)
        {
            if (_financialYearService.IsFinancialYearExists(model.Abbr.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "FinancialYear already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _financialYearService.EditFinancialYear(model);
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
            var res = await _financialYearService.GetAllFinancialYears();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{financialYearId}")]
        public async Task<ActionResult> Delete(Guid financialYearId)
        {
            var isDeleted = await _financialYearService.DeleteFinancialYear(financialYearId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{financialYearId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid financialYearId)
        {
            var data = await _financialYearService.GetFinancialYearById(financialYearId);
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
            List<SelectListDTO> res = _financialYearService.GetFinancialYearSelectList();
            return Ok(res);
        }

        [Route("GetMaxOfToDate")]
        [HttpGet]
        public IActionResult GetMaxOfToDate()
        {
            var maxEndDate = _financialYearService.GetFinancialYearFromToDate();
            return Ok(maxEndDate != null ? maxEndDate.Value.AddDays(1) : new DateTime(DateTime.Now.Year, 1, 1));
        }
        #endregion
    }
}

