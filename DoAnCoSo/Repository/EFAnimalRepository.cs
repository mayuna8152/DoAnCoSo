using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repository
{
    public class EFAnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals.Include(x => x.ClassAnimal).ToListAsync();
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            return await _context.Animals.Include(x => x.ClassAnimal).SingleOrDefaultAsync(x => x.IdAnimal == id);
        }

        public async Task AddAsync(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var  animal = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
        }

		public async Task<IEnumerable<Animal>> SearchExactAsync(string searchTerm)
		{
			// Thực hiện tìm kiếm chính xác dựa trên từ khóa
			return await _context.Animals.Where(x => x.Name.Equals(searchTerm)).ToListAsync();
		}
	}
}
