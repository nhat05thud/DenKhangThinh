using DenKhangThinh.Models.Search;

namespace DenKhangThinh.Services;

public interface ISearchService
{
    public SearchResponseModel Search(SearchRequestModel searchRequest);
}