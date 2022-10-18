using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services.BusinessService;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Service.Mapping;

namespace NLayerApp.Service.Services.BusinessService
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryResponseDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productsDto = ObjectMapper.Mapper.Map<List<ProductWithCategoryResponseDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryResponseDto>>.Success(200, data: productsDto, isSuccess: true);
        }

        public async Task<CustomResponseDto<ProductWithCategoryResponseDto>> GetProductByGrandTotal(decimal grandTotal)
        {
            var product = await _productRepository.GetProductByGrandTotal(grandTotal);
            var productDto = ObjectMapper.Mapper.Map<ProductWithCategoryResponseDto>(product);
            return CustomResponseDto<ProductWithCategoryResponseDto>.Success(200, data: productDto, isSuccess: true);
        }
    }
}
