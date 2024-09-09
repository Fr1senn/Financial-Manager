using System.Net;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetCategoriesAsync(
            [FromQuery] int packSize = 10,
            [FromQuery] int pageNumber = 0
        )
        {
            try
            {
                return Ok(
                    ApiResponse<IEnumerable<Category>>.Succeed(
                        await _categoryRepository.GetCategoriesAsync(packSize, pageNumber)
                    )
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
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
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
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
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
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
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.CreateCategoryAsync(category);
                return Ok(ApiResponse.Succeed());
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                return Ok(ApiResponse.Succeed());
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
