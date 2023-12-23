using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("BusinessUnit")]
    public class BusinessUnitController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IBusinessUnitService _businessUnitsService;
        #endregion

        #region Constructor
        public BusinessUnitController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IBusinessUnitService businessUnitService) : base(contextAccessor)
        {
            _context = context;
            _businessUnitsService = businessUnitService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(BusinessUnitDTO model)
        {
            if (_businessUnitsService.IsBusinessUnitExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessUnit already exists." });
            }
            model.CreatedById = currentUser.Id;
           // model.CreatedById = Guid.Empty;
            Guid id = await _businessUnitsService.AddBusinessUnit(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(BusinessUnitDTO model)
        {
            if (_businessUnitsService.IsBusinessUnitExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessUnit already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _businessUnitsService.EditBusinessUnit(model);
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
            var res = await _businessUnitsService.GetAllBusinessUnit();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{BussinessUnitId}")]
        public async Task<ActionResult> Delete(Guid BussinessUnitId)
        {
            var isDeleted = await _businessUnitsService.DeleteBusinessUnit(BussinessUnitId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{bussinessUnitId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid bussinessUnitId)
        {
            var data = await _businessUnitsService.GetBusinessUnitById(bussinessUnitId);
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
            List<SelectListDTO> res = _businessUnitsService.GetBusinessUnitSelectList();
            return Ok(res);
        }

        [Route("GetAllByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetBySocietyBuildingId(Guid companyId)
        {
            var res = await _businessUnitsService.GetBusinessUnitByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetParentBusinessUnitSelectList")]
        [HttpGet]
        public async Task<IActionResult> GetParentBusinessUnitSelectList(Guid? id, Guid companyId)
        {
            List<SelectListDTO> res = await _businessUnitsService.GetParentBusinessUnitSelectList(id, companyId);
            return Ok(res);
        }
        #endregion
    }
}