using WebShop.Models;
using Microsoft.AspNetCore.Mvc;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.Areas.Admin.Controllers
{
    

    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
