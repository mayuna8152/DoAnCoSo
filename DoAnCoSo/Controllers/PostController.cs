using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                Posts = posts
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = postViewModel.Post;
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Handle invalid ModelState if necessary
            return View("Index", postViewModel);
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
