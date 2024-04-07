using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repository
{
    public class EFPostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public EFPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(x => x.Comments).ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(x => x.Comments).SingleOrDefaultAsync(x => x.IdPost == id);
        }

        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var post = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
