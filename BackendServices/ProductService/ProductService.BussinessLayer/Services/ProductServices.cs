using ProductService.BussinessLayer.DTOs;
using ProductService.BussinessLayer.ServiceContracts;
using ProductService.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace ProductService.BussinessLayer.Services;

public class ProductServices : IProductService
{
    public Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        throw new NotImplementedException();
    }
}
