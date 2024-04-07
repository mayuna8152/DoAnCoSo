using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repository
{
    public class EFAnimalImageRepository : IAnimalImageRepository
    {
        private readonly ApplicationDbContext _context;
        public EFAnimalImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AnimalImage>> GetAllAsync()
        {
            return await _context.AnimalImages.Include(x => x.Animal).ToListAsync();
        }

        public async Task<AnimalImage> GetByIdAsync(int id)
        {
            return await _context.AnimalImages.Include(x => x.Animal).SingleOrDefaultAsync(x => x.IdAnimal == id);
        }

        public async Task AddAsync(AnimalImage animalImage)
        {
            _context.AnimalImages.Add(animalImage);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(AnimalImage animalImage)
        {
            _context.AnimalImages.Update(animalImage);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var animalImage = await _context.AnimalImages.FindAsync(id);
            _context.AnimalImages.Remove(animalImage);
            await _context.SaveChangesAsync();
        }
    }
}
