using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("Location")]
    public class LocationController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private ILocationService _locationService;
        #endregion

        #region Constructor
        public LocationController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                ILocationService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _locationService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(LocationDTO model)
        {
            if (_locationService.IsLocationExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Location already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _locationService.AddLocation(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(LocationDTO model)
        {
            if (_locationService.IsLocationExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Location already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _locationService.EditLocation(model);
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
            var res = await _locationService.GetAllLocations();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{locationId}")]
        public async Task<ActionResult> Delete(Guid locationId)
        {
            var isDeleted = await _locationService.DeleteLocation(locationId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{locationId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid locationId)
        {
            var data = await _locationService.GetLocationById(locationId);
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
            List<SelectListDTO> res = _locationService.GetLocationSelectList();
            return Ok(res);
        }


        [Route("GetAllByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetLocationByCompanyId(Guid companyId)
        {
            var res = await _locationService.GetLocationByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }
        [Route("GetParentLocationSelectList")]
        [HttpGet]
        public async Task<IActionResult> GetParentLocationSelectList(Guid? id, Guid CompanyId)
        {
            List<SelectListDTO> res = await _locationService.GetParentLocationSelectList(id, CompanyId);
            return Ok(res);
        }
        #endregion
    }
}

