using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
	public class ClassAnimalController : Controller
	{
		private readonly IAnimalRepository _animalRepository;
		private readonly IClassAnimalRepository _classanimalRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IPostRepository _postRepository;


		public ClassAnimalController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository
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
		public async Task<IActionResult> Details(int id)
		{
			var classAnimal = await _classanimalRepository.GetByIdAsync(id);

			if (classAnimal == null)
			{
				return NotFound();
			}

			return View(classAnimal);
		}

	}
}

