using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Strategy;
using DAL.Pagination;
using DAL.UoW;

namespace BLL.Services.Strategy
{
    public class CategoryFilter : IFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryFilter(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedList<ArticleDto>> GetFilteredArticlesAsync(int pageIndex, int pageSize, FilterOptions filterOptions)
        {
            var articles = await _unitOfWork.ArticleRepository.GetAllByCategoryAsync(pageIndex, pageSize, 
                                                                            filterOptions.CategoryId);
            var articlesDto =  _mapper.Map<PagedList<ArticleDto>>(articles);
            return articlesDto;
        }
    }
}