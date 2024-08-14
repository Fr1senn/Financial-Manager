using financial_manager.Models;
using financial_manager.Models.Enums;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] int packSize = 10, [FromQuery] int pageNumber = 0)
        {
            try
            {
                return Ok(new OperationResult<Category>(true, HttpResponseCode.Ok, null, await _categoryRepository.GetCategoriesAsync(packSize, pageNumber)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }

        [HttpGet("GetUserCategoryQuantity")]
        public async Task<IActionResult> GetUserCategoryQuantityAsync()
        {
            try
            {
                int userId = 1;
                return Ok(new
                {
                    isSuccess = true,
                    httpResponseCode = HttpResponseCode.Ok,
                    message = string.Empty,
                    data = await _categoryRepository.GetUserCategoryQuantity(userId),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] int categoryId)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(categoryId);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.CreateCategoryAsync(category);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch(Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }
    }
}
