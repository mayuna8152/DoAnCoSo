using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IClassAnimalRepository _classanimalRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;


        public HomeController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository
            , ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _animalRepository = animalRepository;
            _classanimalRepository = classAnimalRepository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var classanimal = await _classanimalRepository.GetAllAsync();
            return View(classanimal);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
