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

            return View(animals);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var classAnimals = await _classanimalRepository.GetAllAsync();
            ViewBag.ClassAnimals = new SelectList(classAnimals, "IdClass", "Name");
            Debug.WriteLine("Hello: ");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Animal animal, [FromForm] IFormFile Avatar, [FromForm] IFormFile ImgQR3D, [FromForm] IFormFile NoiSinhSongImage, [FromForm] List<IFormFile> AnimalImages)
        {
            if (ImgQR3D != null && NoiSinhSongImage != null && Avatar != null)
            {
                animal.ImgQR3D = await Save3D(ImgQR3D);
                animal.NoiSinhSongImage = await SaveNoiSinhSong(NoiSinhSongImage);
                animal.Avatar = await SaveAVT(Avatar);
            }
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            if (AnimalImages != null && AnimalImages.Count > 0)
            {
                animal.AnimalImages = new List<AnimalImage>();

                foreach (var image in AnimalImages)
                {
                    var imageUrl = await SaveListImage(image);

                    var animalImage = new AnimalImage
                    {
                        Url = imageUrl,
                        IdAnimal = animal.IdAnimal 
                    };

                    animal.AnimalImages.Add(animalImage);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");



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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            var classAnimals = await _classanimalRepository.GetAllAsync();
            ViewBag.ClassAnimals = classAnimals;
            return View(animal);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            if (id != animal.IdAnimal)
            {
                return NotFound();
            }

            await _animalRepository.UpdateAsync(animal);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _animalRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task<String> Save3D(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/desgin/Animal/3DQR", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create)) { await image.CopyToAsync(fileStream); }
            return image.FileName;
        }
        private async Task<String> SaveAVT(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/desgin/Animal/Avatar", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create)) { await image.CopyToAsync(fileStream); }
            return image.FileName;
        }
        private async Task<String> SaveListImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/desgin/Animal/ListImage", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create)) { await image.CopyToAsync(fileStream); }
            return image.FileName;
        }
        private async Task<String> SaveNoiSinhSong(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/desgin/Animal/NoiSinhSong", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create)) { await image.CopyToAsync(fileStream); }
            return image.FileName;
        }

    }

}
