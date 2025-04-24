using DenKhangThinh.Extensions;
using DenKhangThinh.Models.Search;
using Examine;
using Examine.Search;
using Lucene.Net.Analysis.Core;
using Umbraco.Cms.Infrastructure.Examine;

namespace DenKhangThinh.Services;

public class SearchService : ISearchService
{
    private readonly IExamineManager _examineManager;
    private readonly string[] _docTypesToInclude = new[] { Product.ModelTypeAlias };

    public SearchService(IExamineManager examineManager)
    {
        _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));

    }

    public SearchResponseModel Search(SearchRequestModel searchRequest)
    {
        if (searchRequest == null || !_examineManager.TryGetIndex(UmbracoConstants.UmbracoIndexes.ExternalIndexName, out IIndex? index))
        {
            return new SearchResponseModel();
        }

        var query = index.Searcher.CreateQuery(IndexTypes.Content).GroupedOr(new[] { "__NodeTypeAlias" }, _docTypesToInclude);

        string[]? terms = !string.IsNullOrWhiteSpace(searchRequest.Query)
            ? searchRequest.Query.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(x.ToLower()) && x.Length > 2)
                .ToArray()
            : null;
        if (terms == null) return new SearchResponseModel(searchRequest.Query, 0, null);

        query = query.And().Group(q =>
            q.GroupedOr(new[] { "metaTitle" }, terms.Boost(80))
            .Or()
            .GroupedOr(new[] { "nodeName" }, terms.Boost(70))
            .Or()
            .GroupedOr(new[] { "metaTitle" }, terms.MultipleCharacterWildcard())
            .Or()
            .GroupedOr(new[] { "nodeName" }, terms.MultipleCharacterWildcard())
            .Or()
            .GroupedOr(new[] { "metaDescription" }, terms.Boost(50))
            .Or()
            .GroupedOr(new[] { "description" }, terms.Boost(50))
            .Or()
            .GroupedOr(new[] { "productSizes" }, terms.Boost(40)),
            BooleanOperation.Or);

        ISearchResults? pageOfResults = query.Execute(new QueryOptions(searchRequest.Skip, searchRequest.PageSize));

        return new SearchResponseModel(searchRequest.Query, pageOfResults.TotalItemCount, pageOfResults);
    }
}