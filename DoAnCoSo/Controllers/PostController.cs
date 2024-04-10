using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostRepository _postRepository;
		private readonly ApplicationDbContext _context;


		public PostController(IAnimalRepository animalRepository, IClassAnimalRepository classAnimalRepository
			, ICommentRepository commentRepository, IPostRepository postRepository, ApplicationDbContext context)
		{
			_postRepository = postRepository;
			_context = context;

		}
		public IActionResult Index()
		{
			var posts = _context.Posts.ToList();
			var model = new PostViewModel
			{
				Posts = posts ?? new List<Post>() // Initialize with an empty list if `posts` is null
			};
			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel viewModel, IFormFile imageQRVideo)
        {
            if (imageQRVideo != null && imageQRVideo.Length > 0)
            {
                // Save the image and get the file name
                viewModel.Post.ImageQRVideo = await SaveImage(imageQRVideo);
            }

            // Explicitly mark the ImageQRVideo field as valid
            ModelState.Remove("Post.ImageQRVideo");

            if (ModelState.IsValid)
            {
                _context.Posts.Add(viewModel.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Handle invalid ModelState if necessary
            return View(viewModel);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/desgin/Post", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName;
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            // Load the comments for the post
            

            return View(post);
        }
	}
}
