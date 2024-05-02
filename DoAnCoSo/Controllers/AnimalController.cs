using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;


namespace DoAnCoSo.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IClassAnimalRepository _classanimalRepository;
        private readonly IAnimalImageRepository _animalImageRepository;
        private readonly ApplicationDbContext _context;


        public AnimalController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository, IAnimalImageRepository animalImageRepository, ApplicationDbContext applicationDbContext)
        {
            _animalRepository = animalRepository;
            _classanimalRepository = classAnimalRepository;
            _animalImageRepository = animalImageRepository;
            _context = applicationDbContext;

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

			ViewBag.SearchTerm = searchTerm; // Add this line to pass the search term to the view

			return View(animals);
		}
		public async Task<IActionResult> FindAnimal( )
        {
            
			return View();
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
