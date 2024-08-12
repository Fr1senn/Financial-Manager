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
    }
}
