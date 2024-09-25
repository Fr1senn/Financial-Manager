using System.Net;
using financial_manager.Entities.DTOs;
using financial_manager.Entities.Requests;
using financial_manager.Entities.Shared;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] PageRequest request)
        {
            try
            {
                return Ok(
                    ApiResponse<IEnumerable<CategoryDTO>>.Succeed(
                        await _categoryRepository.GetCategoriesAsync(request)
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpGet("GetUserCategoryQuantity")]
        public async Task<IActionResult> GetUserCategoryQuantityAsync()
        {
            try
            {
                return Ok(
                    ApiResponse<int>.Succeed(await _categoryRepository.GetUserCategoryQuantity())
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpGet("GetTotalCategoriesConsumption")]
        public async Task<IActionResult> GetTotalCategoriesConsumptionAsync()
        {
            try
            {
                return Ok(
                    ApiResponse<Dictionary<string, decimal>>.Succeed(
                        await _categoryRepository.GetTotalCategoriesConsumption()
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] int categoryId)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(categoryId);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryRequest request)
        {
            try
            {
                await _categoryRepository.CreateCategoryAsync(request);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryRequest request)
        {
            try
            {
                await _categoryRepository.UpdateCategoryAsync(request);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
