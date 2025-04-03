using ProductAPI.Application.Interfaces;
using ProductAPI.Domain.Entities;
using ProductAPI.Domain.Interfaces;

namespace ProductAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Product?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<int> AddAsync(Product product) => _repository.AddAsync(product);
        public Task<bool> UpdateAsync(Product product) => _repository.UpdateAsync(product);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
