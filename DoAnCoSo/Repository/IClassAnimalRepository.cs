using DoAnCoSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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