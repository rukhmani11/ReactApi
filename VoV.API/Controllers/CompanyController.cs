using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{

    [Route("Company")]
    public class CompanyController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private ICompanyService _companiesService;
        #endregion

        #region Constructor
        public CompanyController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                ICompanyService CompaniesService) : base(contextAccessor)
        {
            _context = context;
            _companiesService = CompaniesService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var httpRequest = HttpContext.Request;
            var model = JsonConvert.DeserializeObject<CompanyDTO>(httpRequest.Form["data"]);

            if (model == null)
            {
                return BadRequest(new { isSuccess = false, message = "company object is required." });
            }
            if (_companiesService.IsCompanyExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Company already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _companiesService.AddCompany(model, httpRequest.Form.Files);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit()
        {
            var httpRequest = HttpContext.Request;
            var model = JsonConvert.DeserializeObject<CompanyDTO>(httpRequest.Form["data"]);

            if (model == null)
            {
                return BadRequest(new { isSuccess = false, message = "company object is required." });
            }
            if (_companiesService.IsCompanyExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "Company already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _companiesService.EditCompany(model, httpRequest.Form.Files);
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
            var res = await _companiesService.GetAllCompany();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult> Delete(Guid companyId)
        {
            var isDeleted = await _companiesService.DeleteCompany(companyId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid companyId)
        {
            var data = await _companiesService.GetCompanyById(companyId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("ActivateOrDeActivate/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> ActivateOrDeActivate(Guid companyId)
        {
            var tuple = await _companiesService.ActivateOrDeActivate(companyId, currentUser.Id);
            return Ok(new { isSuccess = tuple.Item1, message = tuple.Item2 });
        }

        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _companiesService.GetCompanySelectList();
            return Ok(res);
        }

        #endregion
    }
}

