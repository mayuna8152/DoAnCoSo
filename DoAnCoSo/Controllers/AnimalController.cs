using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;


namespace DoAnCoSo.Controllers
{
    public class AnimalController : Controller
    {
		private readonly IAnimalRepository _animalRepository;
		private readonly IClassAnimalRepository _classanimalRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IPostRepository _postRepository;


		public AnimalController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository
			, ICommentRepository commentRepository, IPostRepository postRepository)
		{
			_animalRepository = animalRepository;
			_classanimalRepository = classAnimalRepository;
			_commentRepository = commentRepository;
			_postRepository = postRepository;
		}
        public async Task<IActionResult> Index(string searchTerm)
        {
            IEnumerable<Animal> animals;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                animals = await _animalRepository.SearchExactAsync(searchTerm);
            }
            else
            {
                animals = await _animalRepository.GetAllAsync();
            }

            if (animals == null)
            {
                return NotFound();
            }

            return View(animals);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }
        public async Task<IActionResult> Details(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);

        }
 

    }
        
}
