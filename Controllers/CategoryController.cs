using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located

namespace SocialMedia.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid model");
            }

            var newCategory = await _categoryService.AddNewCategory(model.Name);
            return Ok(newCategory);
        }
    }
}
