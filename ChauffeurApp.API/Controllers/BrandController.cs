using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseAPIController
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("CreateBrand")]
        public async Task<IActionResult> CreateBrand(BrandCreateDTO createBrandDTO)
        {
            return HandleResult(await _brandService.CreateBrand(createBrandDTO));
        }

        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            return HandleResult(await _brandService.ViewAllBrands());
        }

        [HttpGet("GetBrandByID")]
        public async Task<IActionResult> GetBrandByID(long ID)
        {
            return HandleResult(await _brandService.ViewBrandByID(ID));
        }

        [HttpDelete("Deletebrand")]
        public async Task<IActionResult> DeleteBrand(long ID)
        {
            return HandleResult(await _brandService.DeleteBrand(ID));   
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(BrandCreateDTO updateBrandDTO, long ID)
        {
            return HandleResult(await _brandService.UpdateBrand(updateBrandDTO, ID));
        }
    }
}
