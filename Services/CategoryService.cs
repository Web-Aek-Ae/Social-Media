using SocialMedia.Models.Database;
using System.Linq;

namespace SocialMedia.Services
{
    public class CategoryService
    {
        private readonly SocialMediaContext _context;

        public CategoryService(SocialMediaContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        // Method to add a new category if it doesn't already exist
        public async Task<Category> AddNewCategory(string categoryName)
        {
            // Check if the category already exists
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());

            if (existingCategory == null)
            {
                // Category doesn't exist, so create a new one
                var newCategory = new Category { Name = categoryName };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync(); // Save changes to the database

                return newCategory; // Return the newly created category
            }

            // Return the existing category if it already exists
            return existingCategory;
        }
    }
}
