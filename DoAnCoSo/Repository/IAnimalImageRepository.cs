using DoAnCoSo.Models;

namespace DoAnCoSo.Repository
{
    public interface IAnimalImageRepository
    {
        Task<IEnumerable<AnimalImage>> GetAllAsync();
        Task<AnimalImage> GetByIdAsync(int id);
        Task AddAsync(AnimalImage animalImage);
        Task UpdateAsync(AnimalImage animalImage);
        Task DeleteAsync(int id);
    }
}
