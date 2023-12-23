//using Microsoft.AspNetCore.Mvc;
//using VoV.Data.Context;
//using VoV.Data.DTOs;
//using VoV.Services.Interface;
//using VoV.Services.Service;

//namespace VoV.API.Controllers
//{
//    [Route("CompanyObservation")]
//    public class CompanyObservationController : BaseApiController
//    {
//        #region Properties
//        private readonly VoVDbContext _context;
//        private ICompanyObservationService _companyObservationsService;
//        #endregion

//        #region Constructor
//        public CompanyObservationController(IHttpContextAccessor contextAccessor,
//                VoVDbContext context,
//                ICompanyObservationService businessSegmentsService) : base(contextAccessor)
//        {
//            _context = context;
//            _companyObservationsService = businessSegmentsService;
//        }
//        #endregion

//        #region Methods
//        [Route("Add")]
//        [HttpPost]
//        public async Task<IActionResult> Add(CompanyObservationDTO model)
//        {
//            if (_companyObservationsService.IsCompanyObservationExists(model.Name.Trim(), model.Id))
//            {
//                return BadRequest(new { isSuccess = false, message = "CompanyObservation already exists." });
//            }
//            //model.CreatedById = currentUser.Id;
//            model.CreatedById = Guid.Empty;
//            Guid id = await _companyObservationsService.AddCompanyObservation(model);
//            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
//        }

//        [Route("Edit")]
//        [HttpPut]
//        public async Task<IActionResult> Edit(CompanyObservationDTO model)
//        {
//            if (_companyObservationsService.IsCompanyObservationExists(model.Name.Trim(), model.Id))
//            {
//                return BadRequest(new { isSuccess = false, message = "CompanyObservation already exists." });
//            }
//            model.UpdatedById = currentUser.Id;
//            Guid? id = await _companyObservationsService.EditCompanyObservation(model);
//            if (id == null || id == Guid.Empty)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, message = "Successfully updated record." });
//        }

//        [Route("GetAll")]
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var res = await _companyObservationsService.GetAllCompanyObservations();
//            return Ok(new { isSuccess = true, list = res });
//        }

//        [HttpDelete("{companyObservationId}")]
//        public async Task<ActionResult> Delete(Guid companyObservationId)
//        {
//            var isDeleted = await _companyObservationsService.DeleteCompanyObservation(companyObservationId);
//            if (!isDeleted)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
//        }

//        [Route("GetById/{companyObservationId}")]
//        [HttpGet]
//        public async Task<IActionResult> GetById(Guid companyObservationId)
//        {
//            var data = await _companyObservationsService.GetCompanyObservationById(companyObservationId);
//            if (data == null)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, data = data });
//        }
//        [Route("GetAllByCompanyId/{companyId}")]
//        [HttpGet]
//        public async Task<IActionResult> GetByCompanyId(Guid companyId)
//        {
//            var res = await _companyObservationsService.GetCompanyObservationByCompanyId(companyId);
//            return Ok(new { isSuccess = true, list = res });
//        }
//        [Route("GetSelectList")]
//        [HttpGet]
//        public IActionResult GetSelectList()
//        {
//            List<SelectListDTO> res = _companyObservationsService.GetCompanyObservationSelectList();
//            return Ok(res);
//        }
//        #endregion
//    }
//}

