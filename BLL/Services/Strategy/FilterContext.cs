using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using BLL.Dtos;
using BLL.Interfaces.Strategy;
using DAL.Pagination;

namespace BLL.Services.Strategy
{
    public class FilterContext
    {
        private IFilter _strategy;
        public FilterContext()
        {
        }
        public FilterContext(IFilter strategy)
        {
          _strategy = strategy;
        }
        
        public void SetStrategy(IFilter strategy)
        { 
            _strategy = strategy;
        }
        
        public async Task<PagedList<ArticleDto>> FindArticles(int pageIndex, int pageSize, FilterOptions filterOptions)
        {
            var result = await _strategy.GetFilteredArticlesAsync(pageIndex, pageSize, filterOptions);
            return result;
        }
    }
}