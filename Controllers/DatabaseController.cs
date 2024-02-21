using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly DatabaseService _databaseService;

        public DatabaseController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Tables()
        {
            var tableNames = await _databaseService.GetTableNamesAsync();
            var model = new TableViewModel
            {
                TableNames = tableNames
            };
            return View(model); // Pass the model to the view
        }
    }
}
