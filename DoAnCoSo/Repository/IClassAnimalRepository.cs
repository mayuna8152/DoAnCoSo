using DoAnCoSo.Models;

namespace DoAnCoSo.Repository
{
    public interface IClassAnimalRepository
    {
        Task<IEnumerable<ClassAnimal>> GetAllAsync();
        Task<ClassAnimal> GetByIdAsync(int id);
        Task AddAsync(ClassAnimal classAnimal);
        Task UpdateAsync(ClassAnimal classAnimal);
        Task DeleteAsync(int id);
    }
}
