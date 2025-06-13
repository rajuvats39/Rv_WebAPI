using Microsoft.AspNetCore.Mvc;
using Rv_WebAPI.Models.Data;
using Rv_WebAPI.Models.Entity;
using Rv_WebAPI.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rv_WebAPI.Controllers
{
    [Route("api/Catagory")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryBlogApp<BlogAppCatagotyModel> _repository;
        public CategoriesController(IRepositoryBlogApp<BlogAppCatagotyModel> repository)
        {
            _repository = repository;
        }

        [HttpGet("GetCategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _repository.GetAllAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound(new ResponseMessage { IsSuccess = false, Message = "No categories found.", Data = null });
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Category list retrieved successfully.", Data = categories });
        }
    }
}