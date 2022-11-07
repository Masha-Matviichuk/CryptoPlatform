using System.Security.Claims;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Paging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        //todo: add error handling, validate input params, return not only Ok, logging, authorization header 
        public NewsController(IMapper mapper, IArticleService articleService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _articleService = articleService;
            _categoryService = categoryService;
        }
        
        // GET api/news/pageIndex=1/pagesize=1
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public async Task<IActionResult> GetAllNews([FromQuery] PagingOptions pagingOptions)
        {
            if (pagingOptions.PageIndex < 0 || pagingOptions.PageSize <= 0)
            {
                return BadRequest("Not valid paging options!");
            }
            var articles = await _articleService.GetAllArticlesAsync(pagingOptions.PageIndex, pagingOptions.PageSize);
            return Ok(articles);
        }
        
        // GET api/news/filter/pageIndex=1/pagesize=1
        [HttpGet("/filter/{pageIndex:int}/{pageSize:int}")]
        public async Task<IActionResult> GetAllNewsWithFilters([FromQuery] PagingOptions pagingOptions, FilterOptions filterOptions)
        {
            //todo: validate filter options
            if (pagingOptions.PageIndex < 0 || pagingOptions.PageSize <= 0)
            {
                return BadRequest("Not valid paging options!");
            }
            if(filterOptions.CategoryId == null && filterOptions.DateFrom == null  && filterOptions.DateTo == null)
               return Ok(Url.Link(nameof(GetAllNews), null));
            var articles = await _articleService.GetArticlesByFilterAsync(pagingOptions.PageIndex, pagingOptions.PageSize, filterOptions);
            return Ok(articles);
        }
        
        // GET api/news/1
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Not valid id!");
            }
            var articles = await _articleService.GetArticleByIdAsync(id);
            return Ok(articles);
        }

        
        //todo: pass as query parameters
        // GET api/news/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories([FromQuery] PagingOptions pagingOptions)
        {
            if (pagingOptions.PageIndex < 0 || pagingOptions.PageSize <= 0)
            {
                return BadRequest("Not valid paging options!");
            }
            var categories = await _categoryService.GetAllCategoriesAsync(pagingOptions.PageIndex, pagingOptions.PageSize);
            return Ok(categories);
        }
        
        // PUT api/news/add_like
        [HttpPut("add_like")]
        public async Task<IActionResult> AddLike(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                return BadRequest("Not valid id!");
            }
            string? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var result = await _articleService.AddLikeAsync(articleId, userId);
            return Ok(result);
        }
        
        // PUT api/news/add_dislike
        [HttpPut("add_dislike")]
        public async Task<IActionResult> AddDislike(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                return BadRequest("Not valid id!");
            }
            string? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var result = await _articleService.AddDislikeAsync(articleId, userId);
            return Ok(result);
        }
    }
}