using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repository
{
    public class EFClassAnimalRepository : IClassAnimalRepository
    {
        private readonly ApplicationDbContext _context;
        public EFClassAnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClassAnimal>> GetAllAsync()
        {
            return await _context.ClassAnimals.Include(x => x.Animals).ToListAsync();
        }

        public async Task<ClassAnimal> GetByIdAsync(int id)
        {
            return await _context.ClassAnimals.Include(x => x.Animals).SingleOrDefaultAsync(x => x.IdClass == id);
        }

        public async Task AddAsync(ClassAnimal classAnimal)
        {
            _context.ClassAnimals.Add(classAnimal);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(ClassAnimal classAnimal)
        {
            _context.ClassAnimals.Update(classAnimal);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var classAnimal = await _context.ClassAnimals.FindAsync(id);
            _context.ClassAnimals.Remove(classAnimal);
            await _context.SaveChangesAsync();
        }
    }
}
