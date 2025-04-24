using DenKhangThinh.Configuration;
using DenKhangThinh.Helpers;
using DenKhangThinh.Models.ContentModels;
using DenKhangThinh.Models.Search;
using DenKhangThinh.Models.ViewModels;
using DenKhangThinh.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;

using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace DenKhangThinh.Controllers.Render;

public class SearchController : RenderController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISearchService _searchService;
    private readonly SiteConfig _siteConfig;

    public SearchController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IHttpContextAccessor httpContextAccessor,
        ISearchService searchService,
        IOptions<SiteConfig> siteConfig) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _searchService = searchService;
        _siteConfig = siteConfig.Value;
    }

    public override IActionResult Index()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var query = httpContext?.Request.Query[Constants.QueryStrings.Query];
        var page = httpContext?.Request.Query[Constants.QueryStrings.Page];

        if (CurrentPage == null) return BadRequest();

        var pageSize = _siteConfig?.SearchSettings?.PageSize ?? Constants.Search.DefaultPageSize;

        var searchRequest = new SearchRequestModel(query, page, pageSize);

        var searchResponse = _searchService.Search(searchRequest);

        var pagination = new PaginationViewModel
        {
            TotalResults = searchResponse.TotalResultCount,
            TotalPages = (int)Math.Ceiling((double)(searchResponse.TotalResultCount / searchRequest.PageSize)),
            ResultsPerPage = searchRequest.PageSize,
            CurrentPage = searchRequest.Page,
            PaginationUrlFormat = PaginationHelper.GetPaginationUrlFormat(Request.Path, Request?.QueryString.ToString(), page)
        };

        var model = new SearchPageContentModel(CurrentPage)
        {
            SearchRequest = searchRequest,
            SearchResponse = searchResponse,
            Pagination = pagination
        };

        return CurrentTemplate(model);
    }
}