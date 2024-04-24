using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DoAnCoSo.Models;
using DoAnCoSo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnCoSo.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IPostRepository _postRepository;
		private readonly ApplicationDbContext _context;
		private readonly HttpClient _httpClient;

		public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, ApplicationDbContext context)
		{
			_commentRepository = commentRepository;
			_postRepository = postRepository;
			_context = context;
			_httpClient = new HttpClient();
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

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddComment(int IdPost, string ChatData)
		{
			var post = await _postRepository.GetByIdAsync(IdPost);
			if (post == null)
			{
				return NotFound();
			}

			int isToxic = await IsToxicComment(ChatData);

			if (isToxic == 1)
			{
				TempData["AlertType"] = "alert-danger";
				TempData["AlertMessage"] = "Warning: Hãy bình luận có văn hóa.";
				return RedirectToAction("Details", "Post", new { id = IdPost });
			}
			else
			{
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


		private async Task<int> IsToxicComment(string comment)
		{
			using (HttpClient client = new HttpClient())
			{
				string apiUrl = "http://localhost:8000/predict";

				// Create a JSON payload with the comment data
				var payload = new StringContent($"\"{comment}\"", Encoding.UTF8, "application/json");

				HttpResponseMessage response = await client.PostAsync(apiUrl, payload);

				if (response.IsSuccessStatusCode)
				{
					string result = await response.Content.ReadAsStringAsync();

					if (result.Contains("toxic") || result.Contains("offensive"))
					{	
						return 1;
					}

				}

				return 0;
			}
		}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var posts = await _postRepository.GetAllAsync();
            ViewBag.Posts = new SelectList(posts, "IdPost", "Title");
            Debug.WriteLine("Hello: ");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Comment comment)
        {
            
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var posts = await _postRepository.GetAllAsync();
            ViewBag.Posts = new SelectList(posts, "IdPost", "Title");
            Debug.WriteLine("Fail to add ");
            return View(comment);
        }
    }
}