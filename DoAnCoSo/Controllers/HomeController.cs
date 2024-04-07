using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IClassAnimalRepository _classanimalRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ApplicationDbContext _context;


        public HomeController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository
            , ICommentRepository commentRepository, IPostRepository postRepository, ApplicationDbContext context)
        {
            _animalRepository = animalRepository;
            _classanimalRepository = classAnimalRepository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _context = context;
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

        public IActionResult Animal(string searchTerm)
        {
            IEnumerable<Animal> animals;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                animals = _context.Animals.Where(a => a.Name.Contains(searchTerm));
            }
            else
            {
                animals = _context.Animals;
            }

            return View(animals);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
