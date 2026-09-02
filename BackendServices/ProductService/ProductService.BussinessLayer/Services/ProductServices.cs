using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductService.BussinessLayer.DTOs;
using ProductService.BussinessLayer.ServiceContracts;
using ProductService.DataAccessLayer.Entities;
using ProductService.DataAccessLayer.RepositoriesContracts;
using System.Linq.Expressions;

namespace ProductService.BussinessLayer.Services;

public class ProductServices : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;

    public ProductServices(IProductRepository repository, 
                            IMapper mapper, 
                            IValidator<ProductAddRequest> productAddRequestValidator,
                            IValidator<ProductUpdateRequest> productUpdateRequestValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
    }

    #region CUD
    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }
        ValidationResult validation = await _productAddRequestValidator.ValidateAsync(productAddRequest);
        if (!validation.IsValid)
        {
            IEnumerable<string> errorMessage = validation.Errors.Select(vf => vf.ErrorMessage);
            string commaSepratedErrorMessage = string.Join(", ", errorMessage);
            throw new ArgumentException(commaSepratedErrorMessage);
        }

            Product product = _mapper.Map<Product>(productAddRequest);
            Product? addedProduct = await _repository.AddProduct(product);

            if (addedProduct == null)
            {
                return null;
            }
            return _mapper.Map<ProductResponse?>(addedProduct);
        
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        if (productUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(productUpdateRequest));
        }
        ValidationResult validation = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validation.IsValid)
        {
            IEnumerable<string> errorMessage = validation.Errors.Select(vf => vf.ErrorMessage);
            string commaSepratedErrorMessage = string.Join(", ", errorMessage);
            throw new ArgumentException(commaSepratedErrorMessage);
        }

        Product product = _mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct = await _repository.AddProduct(product);

        if (updatedProduct == null)
        {
            return null;
        }
        return _mapper.Map<ProductResponse?>(updatedProduct);

    }


    public async Task<bool> DeleteProduct(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }
        bool isDeleted = await _repository.DeleteProduct(productId);
        return isDeleted;
    }

    #endregion

    #region GET
    public async Task<ProductResponse> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
      Product product = await _repository.GetProductByCondition(conditionExpression);
        if (product == null)
        {
            return null;
        }
       
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<List<ProductResponse>> GetProducts()
    {
     IEnumerable<Product> product = await _repository.GetProducts();
        if (product == null)
        {
            throw new ArgumentException(nameof(product));
        }
        return _mapper.Map<List<ProductResponse>>(product);
    }

    public async Task<List<ProductResponse>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product> product = await _repository.GetProductsByCondition(conditionExpression);
        if (product == null)
        {
            throw new ArgumentException(nameof(product));
        }
        return _mapper.Map<List<ProductResponse>>(product);
    }

    #endregion
}
