using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseAPIController

    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany(CreateCompanyDTO createCompanyDTO)
        {
            return HandleResult(await _companyService.CreateCompany(createCompanyDTO));
        }

        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return HandleResult(await _companyService.ViewAllCompanies());
        }

        [HttpGet("GetCompanyByID")]
        public async Task<IActionResult> GetCompanyByID(long ID)
        {
            return HandleResult(await _companyService.ViewCompanyById(ID));
        }

        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(long ID)
        {
            return HandleResult(await _companyService.DeleteCompany(ID));
        }

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(CreateCompanyDTO updateCompanyDTO, long ID)
        {
            return HandleResult(await _companyService.UpdateCompany(updateCompanyDTO, ID));
        }
    }
}
