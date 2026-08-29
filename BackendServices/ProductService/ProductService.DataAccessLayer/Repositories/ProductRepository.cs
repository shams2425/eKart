using Microsoft.EntityFrameworkCore;
using ProductService.DataAccessLayer.Context;
using ProductService.DataAccessLayer.Entities;
using ProductService.DataAccessLayer.RepositoriesContracts;
using System.Linq.Expressions;

namespace ProductService.DataAccessLayer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Product> AddProduct(Product product)
    {
        if (product == null)
        {
            return null;
        }
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
      Product? product = await _db.Products.FindAsync(productId);

           if (product == null)
            {
                return false;
            }

       _db.Products.Remove(product);
       await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Product> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _db.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await  _db.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
       return await _db.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product> UpdateProduct(Product product)
    {
      Product? existingProduct = await _db.Products.FindAsync(product.ProductID);
        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.Category = product.Category;
        existingProduct.QuantityInStock = product.QuantityInStock;

        await _db.SaveChangesAsync();
        if (existingProduct == null)
        {
            return null;
        }
        return existingProduct;
    }
}
