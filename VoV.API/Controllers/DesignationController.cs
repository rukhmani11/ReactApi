using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("Designation")]
    public class DesignationController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IDesignationService _designationsservice;
        #endregion

        #region Constructor
        public DesignationController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IDesignationService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _designationsservice = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(DesignationDTO model)
        {
            if (_designationsservice.IsDesignationExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Designation already exists." });
            }
            //model.CreatedById = currentUser.Id;
            model.CreatedById = Guid.Empty;
            Guid id = await _designationsservice.AddDesignation(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record   .", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(DesignationDTO model)
        {
            if (_designationsservice.IsDesignationExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Designation already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _designationsservice.EditDesignation(model);
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
            var res = await _designationsservice.GetAllDesignations();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{designationId}")]
        public async Task<ActionResult> Delete(Guid designationId)
        {
            var isDeleted = await _designationsservice.DeleteDesignation(designationId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{designationId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid designationId)
        {
            var data = await _designationsservice.GetDesignationById(designationId);
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
            List<SelectListDTO> res = _designationsservice.GetDesignationSelectList();
            return Ok(res);
        }

        [Route("GetAllByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllByCompanyId(Guid companyId)
        {
            var res = await _designationsservice.GetDesignationByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }
        [Route("GetParentDesignationsSelectList")]
        [HttpGet]
        public async Task<IActionResult> GetParentDesignationsSelectList(Guid? id, Guid companyId)
        {
            List<SelectListDTO> res = await _designationsservice.GetParentDesignationsSelectList(id, companyId);
            return Ok(res);
        }
        #endregion
    }
}


