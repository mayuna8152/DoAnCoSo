using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAnCoSo.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ApplicationDbContext _context;


        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, ApplicationDbContext context)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var cmt = await _commentRepository.GetAllAsync();
            return View(cmt);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cmt = await _commentRepository.GetByIdAsync(id);

            if (cmt == null)
            {
                return NotFound();
            }

            return View(cmt);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost]
		public async Task<IActionResult> AddComment(int IdPost, string ChatData)
		{
			var post = await _postRepository.GetByIdAsync(IdPost);

			if (post == null)
			{
				return NotFound();
			}

			var comment = new Comment
			{
				ChatData = ChatData,
				IdPost = IdPost,
				DateTime = DateTime.Now,
			};

			_context.Comments.Add(comment);
			_context.SaveChanges();

			return RedirectToAction("Details", "Post", new { id = IdPost });
		}
	}
}
