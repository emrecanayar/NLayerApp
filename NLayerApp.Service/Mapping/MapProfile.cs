using AutoMapper;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            #region ProductsMapping
            CreateMap<Product, ProductReadResponseDto>().ReverseMap();
            CreateMap<Product, ProductCreateResponseDto>().ReverseMap();
            CreateMap<Product, ProductCreateRequestDto>().ReverseMap();
            CreateMap<Product, ProductUpdateRequestDto>().ReverseMap();
            CreateMap<Product, ProductUpdateRequestDto>().ReverseMap();
            CreateMap<Product, ProductWithCategoryResponseDto>().ReverseMap();
            #endregion

            #region CategoriesMapping
            CreateMap<Category, CategoryReadResponseDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsResponseDto>().ReverseMap();
            CreateMap<Category, CategoryCreateRequestDto>().ReverseMap();
            CreateMap<Category, CategoryCreateResponseDto>().ReverseMap();
            #endregion

            #region OrdersMapping
            CreateMap<Order, OrderDetailCreateRequestDto>().ReverseMap();
            #endregion

            #region UserMapping
            CreateMap<UserCreateRequestDto, User>().ReverseMap();
            CreateMap<UserResponseDto, User>().ReverseMap();
            #endregion

            #region RoleMapping
            CreateMap<CreateRoleRequestDto, Role>().ReverseMap();
            CreateMap<UpdateRoleRequestDto, Role>().ReverseMap();
            CreateMap<RoleResponseDto, Role>().ReverseMap();
            #endregion


            #region LogMapping
            CreateMap<Log, LogCreateModel>().ReverseMap();
            #endregion
        }
    }
}
