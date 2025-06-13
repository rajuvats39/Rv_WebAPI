using Microsoft.AspNetCore.Mvc;
using Rv_WebAPI.Models.Data;
using Rv_WebAPI.Models.Entity;
using Rv_WebAPI.Utility;

namespace Rv_WebAPI.Controllers
{
    [Route("api/Blogs")]
    [ApiController]
    public class BlogAppController : ControllerBase
    {
        private readonly IRepositoryBlogApp<BlogAppModel> _blogrepository;
        public BlogAppController(IRepositoryBlogApp<BlogAppModel> blogrepository)
        {
            _blogrepository = blogrepository;
        }

        [HttpGet("GetBlogList")]
        public async Task<ActionResult> GetBlogList()
        {
            var blogs = await _blogrepository.GetAllAsync();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Blog list retrieved successfully.", Data = blogs });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById([FromRoute] int id)
        {
            var blog = await _blogrepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound(new ResponseMessage { IsSuccess = false, Message = $"Blog with ID {id} not found." });
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Blog retrieved successfully.", Data = blog });
        }

        [HttpPost("AddBlog")]
        public async Task<IActionResult> AddBlog([FromBody] BlogAppModel blogAppModel)
        {
            if (blogAppModel == null)
            {
                return BadRequest(new ResponseMessage { IsSuccess = false, Message = "Blog data is required.", Data = null });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseMessage { IsSuccess = false, Message = "Invalid blog data.", Data = blogAppModel });
            }
            await _blogrepository.AddAsync(blogAppModel);
            await _blogrepository.SaveChangesAsync();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Blog added successfully.", Data = blogAppModel });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] int id, [FromBody] BlogAppModel blogAppModel)
        {
            if (blogAppModel == null || id != blogAppModel.Id)
            {
                return BadRequest(new ResponseMessage { IsSuccess = false, Message = "Invalid blog data or mismatched ID." });
            }
            var existingBlog = await _blogrepository.GetByIdAsync(id);
            if (existingBlog == null)
            {
                return NotFound(new ResponseMessage { IsSuccess = false, Message = $"Blog with ID {id} not found." });
            }
            // Update fields
            existingBlog.Title = blogAppModel.Title;
            existingBlog.Description = blogAppModel.Description;
            existingBlog.Content = blogAppModel.Content;
            existingBlog.Image = blogAppModel.Image;
            existingBlog.IsFeatured = blogAppModel.IsFeatured;
            _blogrepository.Update(existingBlog);
            await _blogrepository.SaveChangesAsync();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Blog updated successfully.", Data = existingBlog });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            var blog = await _blogrepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound(new ResponseMessage { IsSuccess = false, Message = $"Blog with ID {id} not found." });
            }
            await _blogrepository.DeleteAsync(id);
            await _blogrepository.SaveChangesAsync();
            return Ok(new ResponseMessage { IsSuccess = true, Message = $"Blog with ID {id} deleted successfully." });
        }

        [HttpGet("Featured")]
        public async Task<IActionResult> GetFeaturedBlogs()
        {
            var blogs = await _blogrepository.GetAllAsync(x => x.IsFeatured);
            if (blogs == null || !blogs.Any())
            {
                return NotFound(new ResponseMessage { IsSuccess = false, Message = "No featured blogs found." });
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Featured blogs retrieved successfully.", Data = blogs });
        }
    }
}
