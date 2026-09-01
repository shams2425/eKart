using ProductService.BussinessLayer.DTOs;
using ProductService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProductService.BussinessLayer.ServiceContracts;

public interface IProductService
{
    Task<List<ProductResponse>> GetProducts();
    Task<List<ProductResponse>> GetProductsByCondition(Expression<Func<Product,bool>> conditionExpression);
    Task<ProductResponse> GetProductByCondition(Expression<Func<Product,bool>> conditionExpression);
    Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest);
    Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProduct(Guid productId);
}
