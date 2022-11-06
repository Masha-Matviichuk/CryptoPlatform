using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Strategy;
using DAL.Entities;
using DAL.Pagination;
using DAL.UoW;

namespace BLL.Services.Strategy
{
    public class DateFilter : IFilter
    {  private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DateFilter(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedList<ArticleDto>> GetFilteredArticlesAsync(int pageIndex, int pageSize, FilterOptions filterOptions)
        {
            PagedList<Article> articles = await _unitOfWork.ArticleRepository.GetAllByDateAsync(pageIndex, pageSize, 
                                                                                  filterOptions.DateFrom, filterOptions.DateTo);
            var articlesDto =  _mapper.Map<PagedList<ArticleDto>>(articles);
            return articlesDto;
        }
    }
}