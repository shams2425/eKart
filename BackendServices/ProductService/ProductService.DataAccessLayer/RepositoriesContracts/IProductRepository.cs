using ProductService.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace ProductService.DataAccessLayer.RepositoriesContracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
    Task<Product> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid productId);
}
